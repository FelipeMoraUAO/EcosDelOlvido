using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

    public TextMeshProUGUI timerText;  // Texto en pantalla
    public float tiempoPorFragmento = 90f;
    public float tiempoRestante;
    public float tiempoTotal;
    private bool corriendo = false;

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

    void Start()
    {
        ReiniciarTiempo();
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
                SceneManager.LoadScene("6Derrota");
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
        tiempoRestante = tiempoPorFragmento;
        ActualizarTexto();
    }

    public void IniciarTiempo()
    {
        corriendo = true;
    }

    public void DetenerTiempo()
    {
        corriendo = false;
        tiempoTotal += (tiempoPorFragmento - tiempoRestante);
    }

    public void FragmentoCompletado()
    {
        // Llamar cuando el jugador consiga un fragmento
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
    }
}
