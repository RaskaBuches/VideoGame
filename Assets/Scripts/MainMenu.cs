using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Cambia esto al nombre real
    }

    public void OpenOptions()
    {
        Debug.Log("Opciones abiertas (aquí puedes abrir otro menú)");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        // En el editor de Unity no se cierra, así que muestra este mensaje
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
