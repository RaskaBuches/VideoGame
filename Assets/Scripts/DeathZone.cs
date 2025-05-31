using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo cay� en el DeathZone: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador cay� al vac�o");
            GameOverManager.Instance.ShowGameOver();
        }
    }
}
