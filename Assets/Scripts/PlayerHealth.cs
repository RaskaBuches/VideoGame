using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;

    public int maxHealth = 3;
    private int currentHealth;

    public float invulnerableTime = 1.2f;
    private float lastDamageTime = -999f;

    public HealthUI healthUI; // Referencia al script de la UI

    public GameObject gameOverScreen; // Asigna esto desde el inspector

    public GameObject[] enemies; // Asigna los enemigos desde el Inspector



    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (healthUI != null)
            healthUI.UpdateHearts(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (Time.time - lastDamageTime < invulnerableTime)
            return;

        lastDamageTime = Time.time;
        currentHealth -= amount;

        // Reproduce animación de daño
        if (animator != null)
            animator.SetTrigger("isHurt");

        // Actualiza la UI de corazones
        if (healthUI != null)
            healthUI.UpdateHearts(currentHealth, maxHealth);

        // Si la salud llega a 0, ejecuta Die
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            // Detener movimiento del jugador
            CarlMovement carl = GetComponent<CarlMovement>();
            if (carl != null)
            {
                carl.Die();
            }

            // Mostrar pantalla de Game Over
            if (gameOverScreen != null)
                gameOverScreen.SetActive(true);

            // Buscar todos los enemigos con EnemyController
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            foreach (EnemyController enemy in enemies)
            {
                if (enemy != null)
                {
                    // Desactivar el script para detener patrullas y ataques
                    enemy.enabled = false;

                    // También pausar animaciones si se desea
                    Animator anim = enemy.GetComponent<Animator>();
                    if (anim != null)
                    {
                        anim.speed = 0f;
                    }
                }
            }
        }
    }


}
