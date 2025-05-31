using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform pointA;
    public Transform pointB;
    public Transform player;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;

    private float lastAttackTime = 0f;
    private Vector3 targetPosition;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool wasAttacking = false;
    private bool lastPatrolFlipX;
    private bool isDead = false;

    void Start()
    {
        targetPosition = pointB.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lastPatrolFlipX = false;
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("isAttacking", true);
            animator.SetBool("isWalking", false);

            if (!wasAttacking)
            {
                lastPatrolFlipX = spriteRenderer.flipX;
                wasAttacking = true;
            }

            spriteRenderer.flipX = player.position.x < transform.position.x;

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;

                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                }

                CarlMovement carl = player.GetComponent<CarlMovement>();
                if (carl != null)
                {
                    carl.LoseSword(); // 👈 hacer que Carl pierda la espada
                }
            }
        }
        else
        {
            if (wasAttacking)
            {
                spriteRenderer.flipX = lastPatrolFlipX;
                wasAttacking = false;
            }

            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", true);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
            {
                targetPosition = pointB.position;
                spriteRenderer.flipX = false;
                lastPatrolFlipX = false;
            }
            else if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
            {
                targetPosition = pointA.position;
                spriteRenderer.flipX = true;
                lastPatrolFlipX = true;
            }
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("isDead");

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        StartCoroutine(DelayedDeath(30f));
    }

    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
