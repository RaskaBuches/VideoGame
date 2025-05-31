using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo cayó en el DeathZone: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador cayó al vacío");
            GameOverManager.Instance.ShowGameOver();
        }
    }
}
