using UnityEngine;
using System.Collections;

public class RespawnableItem : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isCollected = false;
    public float respawnDelay = 30f;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Collect()
    {
        if (!isCollected)
        {
            isCollected = true;
            StartCoroutine(RespawnCoroutine()); // 👈 Inicia el proceso
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        // 👇 Ocultar visualmente y desactivar colisiones
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Collider2D col = GetComponent<Collider2D>();
        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        Debug.Log("Espada recogida. Esperando para reaparecer...");

        yield return new WaitForSeconds(respawnDelay);

        // 👇 Reactivar visualmente y colisiones
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        if (sr != null) sr.enabled = true;
        if (col != null) col.enabled = true;

        isCollected = false;

        Debug.Log("¡Espada reapareció!");
    }
}
