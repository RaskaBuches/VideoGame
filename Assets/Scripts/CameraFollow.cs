using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El personaje
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float minX; // Límite izquierdo del fondo
    public float maxX; // Límite derecho del fondo

    void LateUpdate()
    {
        if (target != null)
        {
            float desiredX = target.position.x + offset.x;
            float clampedX = Mathf.Clamp(desiredX, minX, maxX);
            Vector3 desiredPosition = new Vector3(clampedX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
