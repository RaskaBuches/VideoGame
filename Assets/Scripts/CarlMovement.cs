using UnityEngine;
using System.Collections;

public class CarlMovement : MonoBehaviour
{
    public float speed = 5f;
    public float attackRange = 1.2f;
    public Transform attackPoint;
    private Rigidbody2D rb;
    private float moveInput;
    private Animator animator;
    public LayerMask enemyLayer;

    private bool hasSword = false;
    private bool isAttacking = false;
    private bool isDead = false;


    public WeaponUIController weaponUI; // 👈 Referencia pública

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (weaponUI != null)
            weaponUI.HideSwordIcon();
    }

    void Update()
    {
        if (isDead) return; // 👈 Ignora acciones si Carl está muerto
        if (!isAttacking)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                moveInput = 1;
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                moveInput = -1;
            else
                moveInput = 0;

            if (moveInput > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveInput < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            moveInput = 0;
        }

        if (hasSword && Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);

        TryAttackEnemy();

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    private void TryAttackEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyController enemyScript = enemy.GetComponent<EnemyController>();
            if (enemyScript != null)
            {
                enemyScript.Die();
                Debug.Log("¡Enemigo alcanzado y eliminado!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Espada"))
        {
            hasSword = true;
            Debug.Log("¡Espada recogida!");

            // 👇 Lógica para recolectar con reaparición
            RespawnableItem item = other.GetComponent<RespawnableItem>();
            if (item != null)
            {
                item.Collect();
            }

            if (weaponUI != null)
                weaponUI.ShowSwordIcon(); // Mostrar ícono
        }
    }

    public void LoseSword()
    {
        if (hasSword)
        {
            hasSword = false;
            Debug.Log("¡Carl ha perdido la espada!");

            if (weaponUI != null)
                weaponUI.HideSwordIcon(); // Ocultar ícono
        }
    }
    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Disparando animación de muerte"); // 👈 verifica en consola
        animator.SetTrigger("IsDead");

        this.enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        if (weaponUI != null)
            weaponUI.HideSwordIcon();
    }


}
