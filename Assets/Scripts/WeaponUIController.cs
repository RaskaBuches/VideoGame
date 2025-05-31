using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour
{
    public Image swordIcon;

    private void Start()
    {
        swordIcon.gameObject.SetActive(false); // Se oculta al iniciar
    }

    public void ShowSwordIcon()
    {
        swordIcon.gameObject.SetActive(true);
    }

    public void HideSwordIcon()
    {
        swordIcon.gameObject.SetActive(false);
    }
}
