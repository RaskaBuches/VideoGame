using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    public LevelCompleteManager levelCompleteManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelCompleteManager.ShowLevelComplete();
        }
    }
}
