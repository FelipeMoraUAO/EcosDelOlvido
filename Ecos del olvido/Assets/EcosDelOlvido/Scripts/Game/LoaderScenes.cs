using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenes : MonoBehaviour
{
    // Carga una escena por nombre
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Escena del menú principal
    public void Menu()
    {
        SceneManager.LoadScene("1Menu");
    }

    // Escena de instrucciones
    public void Instrucciones()
    {
        SceneManager.LoadScene("2Intrucciones");
    }

    // Escena de créditos
    public void Creditos()
    {
        SceneManager.LoadScene("3Creditos");
    }

    // Escena del Santuario
    public void Santuario()
    {
        SceneManager.LoadScene("4Santuario");
    }

    // Escena del Castillo
    public void Castillo()
    {
        SceneManager.LoadScene("5Castillo");
    }

    // Escena de derrota
    public void Derrota()
    {
        SceneManager.LoadScene("6Derrota");
    }

    // Escena de victoria
    public void Victoria()
    {
        SceneManager.LoadScene("7Victoria");
    }

    // Salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
