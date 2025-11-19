using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    // Instancia estática para usar este script como singleton
    public static GameTimer instance;

    [Header("Configuración general")]
    public TextMeshProUGUI timerText;   // Referencia al texto del cronómetro en pantalla
    public float tiempoTotal;           // Tiempo total acumulado en todo el juego
    private bool corriendo = false;     // Indica si el temporizador está activo

    [Header("Tiempos por escena")]
    public float tiempoSantuario = 60f; // Tiempo asignado para la escena Santuario
    public float tiempoCastillo = 90f;  // Tiempo asignado para la escena Castillo

    private float tiempoRestante;       // Tiempo restante de la escena actual

    // Configura el patrón Singleton y mantiene este objeto entre escenas
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Se ejecuta automáticamente cuando una nueva escena es cargada
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Actualiza el temporizador en cada frame si está corriendo
    void Update()
    {
        if (corriendo)
        {
            tiempoRestante -= Time.deltaTime;

            // Si el tiempo llega a cero, se detiene y carga la escena de derrota
            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                corriendo = false;
                SceneManager.LoadScene("6Derrota");
            }

            ActualizarTexto();
        }
    }

    // Actualiza el texto del cronómetro en pantalla
    void ActualizarTexto()
    {
        if (timerText != null)
        {
            int segundos = Mathf.CeilToInt(tiempoRestante);
            timerText.text = segundos.ToString();
        }
    }

    // Reinicia el tiempo usando el valor asignado para la escena actual
    public void ReiniciarTiempo()
    {
        tiempoRestante = ObtenerTiempoPorEscena();
        ActualizarTexto();
    }

    // Activa el temporizador
    public void IniciarTiempo()
    {
        corriendo = true;
    }

    // Detiene el temporizador y acumula el tiempo usado
    public void DetenerTiempo()
    {
        corriendo = false;
        tiempoTotal += (ObtenerTiempoPorEscena() - tiempoRestante);
    }

    // Se llama cuando el jugador completa un fragmento de recuerdo
    public void FragmentoCompletado()
    {
        DetenerTiempo();
        ReiniciarTiempo();
        IniciarTiempo();
    }

    // Se ejecuta cuando una escena termina de cargar
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Busca el componente de texto con la etiqueta correspondiente
        var found = GameObject.FindWithTag("TimerText");
        if (found != null)
            timerText = found.GetComponent<TextMeshProUGUI>();

        // Reinicia y activa el temporizador al entrar en la nueva escena
        ReiniciarTiempo();
        IniciarTiempo();
    }

    // Devuelve el tiempo inicial según la escena actual
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
                return 90f; // Tiempo por defecto si no coincide ninguna escena conocida
        }
    }
}
