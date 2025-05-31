using UnityEngine;

public class CarlCollisionDebugger : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Carl est� colisionando con: " + collision.collider.name + " en capa: " + LayerMask.LayerToName(collision.collider.gameObject.layer));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Carl entr� en un trigger con: " + other.name + " en capa: " + LayerMask.LayerToName(other.gameObject.layer));
    }
}
