using UnityEngine;

public class GroundCheckGizmo : MonoBehaviour
{
    public float checkRadius = 0.2f;
    public Color gizmoColor = Color.green;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}

