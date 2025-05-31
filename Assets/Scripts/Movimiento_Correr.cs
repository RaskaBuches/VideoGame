using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Detectar movimiento
        float moveInput = Input.GetAxisRaw("Horizontal");

        // true si hay movimiento, false si no
        bool isMoving = Mathf.Abs(moveInput) > 0;

        // Asignar al parámetro del Animator
        anim.SetBool("isRunning", isMoving);
    }
}
