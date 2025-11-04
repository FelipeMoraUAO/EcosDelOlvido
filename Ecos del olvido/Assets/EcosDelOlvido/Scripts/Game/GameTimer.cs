using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

    [Header("Configuración general")]
    public TextMeshProUGUI timerText;   // Texto del cronómetro en pantalla
    public float tiempoTotal;           // Tiempo total acumulado (todas las escenas)
    private bool corriendo = false;

    [Header("Tiempos por escena")]
    public float tiempoSantuario = 60f;
    public float tiempoCastillo = 90f;

    private float tiempoRestante;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (corriendo)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                corriendo = false;
                SceneManager.LoadScene("6Derrota"); // Escena de derrota
            }

            ActualizarTexto();
        }
    }

    void ActualizarTexto()
    {
        if (timerText != null)
        {
            int segundos = Mathf.CeilToInt(tiempoRestante);
            timerText.text = segundos.ToString();
        }
    }

    public void ReiniciarTiempo()
    {
        tiempoRestante = ObtenerTiempoPorEscena();
        ActualizarTexto();
    }

    public void IniciarTiempo()
    {
        corriendo = true;
    }

    public void DetenerTiempo()
    {
        corriendo = false;
        tiempoTotal += (ObtenerTiempoPorEscena() - tiempoRestante);
    }

    public void FragmentoCompletado()
    {
        DetenerTiempo();
        ReiniciarTiempo();
        IniciarTiempo();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar el texto automáticamente al cambiar de escena
        var found = GameObject.FindWithTag("TimerText");
        if (found != null)
            timerText = found.GetComponent<TextMeshProUGUI>();

        // Reiniciar tiempo automáticamente según la nueva escena
        ReiniciarTiempo();
        IniciarTiempo();
    }

    /// <summary>
    /// Devuelve el tiempo inicial según la escena actual.
    /// </summary>
    private float ObtenerTiempoPorEscena()
    {
        string escena = SceneManager.GetActiveScene().name;

        switch (escena)
        {
            case "4Santuario":
                return tiempoSantuario;

            case "5Castillo":
                return tiempoCastillo;

            default:
                return 90f; // Tiempo por defecto
        }
    }
}
