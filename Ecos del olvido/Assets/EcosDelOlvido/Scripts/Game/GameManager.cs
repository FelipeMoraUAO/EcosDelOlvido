using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Escena de Santuario: empieza el conteo
        if (scene.name == "4Santuario")
        {
            GameTimer.instance.ReiniciarTiempo();
            GameTimer.instance.IniciarTiempo();
        }

        // Escena del Castillo: continúa el conteo (ya viene corriendo)
        if (scene.name == "5Castillo")
        {
            GameTimer.instance.IniciarTiempo();
        }

        // Escena final o victoria: se detiene el cronómetro
        if (scene.name == "7Victoria")
        {
            GameTimer.instance.DetenerTiempo();
            Debug.Log("Tiempo total: " + GameTimer.instance.tiempoTotal);
        }
    }
}
