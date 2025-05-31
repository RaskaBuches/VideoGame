using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Sprite heartFull;
    public Sprite heartEmpty;
    public Image[] heartImages;

    public void UpdateHearts(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].sprite = heartFull;
            }
            else
            {
                heartImages[i].sprite = heartEmpty;
            }

            // Ocultar corazones extra si currentHealth < maxHealth
            heartImages[i].enabled = i < maxHealth;
        }
    }
}
