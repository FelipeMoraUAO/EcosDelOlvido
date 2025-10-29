using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // ✅ Singleton accesible globalmente

    // Listas para guardar los ítems recolectados
    public List<string> hechizosRecolectados = new List<string>();
    public List<string> recuerdosRecolectados = new List<string>();

    private void Awake()
    {
        // Asegurar que solo exista un GameManager en toda la ejecución
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Escuchar el cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Escena de Santuario: reinicia y arranca el tiempo
        if (scene.name == "4Santuario")
        {
            if (GameTimer.instance != null)
            {
                GameTimer.instance.ReiniciarTiempo();
                GameTimer.instance.IniciarTiempo();
            }
        }

        // Escena del Castillo: continúa el conteo
        else if (scene.name == "5Castillo")
        {
            if (GameTimer.instance != null)
            {
                GameTimer.instance.IniciarTiempo();
            }
        }

        // Escena final o de victoria: se detiene el cronómetro
        else if (scene.name == "7Victoria")
        {
            if (GameTimer.instance != null)
            {
                GameTimer.instance.DetenerTiempo();
                Debug.Log("Tiempo total: " + GameTimer.instance.tiempoTotal);
            }
        }
    }

    // ✅ Métodos públicos para registrar ítems recolectados
    public void AgregarHechizo(string nombreHechizo)
    {
        if (!hechizosRecolectados.Contains(nombreHechizo))
        {
            hechizosRecolectados.Add(nombreHechizo);
            Debug.Log("Hechizo agregado: " + nombreHechizo);
        }
    }

    public void AgregarRecuerdo(string nombreRecuerdo)
    {
        if (!recuerdosRecolectados.Contains(nombreRecuerdo))
        {
            recuerdosRecolectados.Add(nombreRecuerdo);
            Debug.Log("Recuerdo agregado: " + nombreRecuerdo);
        }
    }
}
