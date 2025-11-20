using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjetoTransicion : MonoBehaviour
{
    [Header("Configuración de interacción")]
    public string escenaDestino = "5Castillo"; // Escena a cargar cuando se cumplan los requisitos
    public int hechizosNecesarios = 2;         // Cantidad mínima de hechizos para avanzar

    [Header("UI")]
    public TextMeshProUGUI textoInteraccion;   // Texto: "Presiona E para avanzar"
    public TextMeshProUGUI textoAviso;         // Texto temporal cuando faltan hechizos

    private bool enRango = false;              // Indica si el jugador está dentro del área del objeto
    private LoaderScenes loader;               // Referencia opcional al sistema de carga de escenas

    void Start()
    {
        // Oculta UI si están asignadas
        if (textoInteraccion != null) textoInteraccion.gameObject.SetActive(false);
        if (textoAviso != null) textoAviso.gameObject.SetActive(false);

        // Intenta encontrar el loader (si existe)
        loader = Object.FindFirstObjectByType<LoaderScenes>();

        Debug.Log("[ObjetoTransicion] Start. escenaDestino=" + escenaDestino +
                  " hechizosNecesarios=" + hechizosNecesarios +
                  " loaderFound=" + (loader != null));
    }

    void Update()
    {
        if (!enRango)
        {
            if (textoInteraccion != null && textoInteraccion.gameObject.activeSelf)
                textoInteraccion.gameObject.SetActive(false);
            return;
        }

        if (textoInteraccion != null)
            textoInteraccion.gameObject.SetActive(true);

        // Al pulsar E intentamos la transición
        if (Input.GetKeyDown(KeyCode.E))
            VerificarYTransicionar();
    }

    void VerificarYTransicionar()
    {
        // Comprobaciones de seguridad
        if (GameManager.Instance == null)
        {
            Debug.LogError("[ObjetoTransicion] GameManager.Instance es null. No se puede verificar progreso.");
            // Fallback: cargar igual la escena si quieres forzar (descomenta la línea siguiente)
            // SceneManager.LoadScene(escenaDestino);
            return;
        }

        int cantidadHechizos = GameManager.Instance.hechizosRecolectados.Count;
        Debug.Log($"[ObjetoTransicion] Intento de transicionar. Hechizos: {cantidadHechizos}/{hechizosNecesarios}");

        if (cantidadHechizos >= hechizosNecesarios)
        {
            Debug.Log("[ObjetoTransicion] Requisitos cumplidos. Transicionando...");

            // Intentamos usar loader si existe y tiene el método, fallback a SceneManager
            if (loader != null)
            {
                try
                {
                    loader.Castillo();
                    Debug.Log("[ObjetoTransicion] Usado loader.Castillo()");
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning("[ObjetoTransicion] loader.Castillo() falló: " + ex.Message + " -> usando SceneManager.LoadScene()");
                    SceneManager.LoadScene(escenaDestino);
                }
            }
            else
            {
                // Fallback directo si no hay loader
                SceneManager.LoadScene(escenaDestino);
                Debug.Log("[ObjetoTransicion] loader no encontrado, usado SceneManager.LoadScene()");
            }
        }
        else
        {
            int faltan = hechizosNecesarios - cantidadHechizos;
            Debug.Log("[ObjetoTransicion] No cumple requisitos, faltan: " + faltan);

            if (textoAviso != null)
                StartCoroutine(MostrarAvisoTemporal($"Aún te faltan {faltan} hechizos..."));
        }
    }

    System.Collections.IEnumerator MostrarAvisoTemporal(string mensaje)
    {
        textoAviso.text = mensaje;
        textoAviso.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        textoAviso.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = true;
            Debug.Log("[ObjetoTransicion] Player entró en rango.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = false;
            Debug.Log("[ObjetoTransicion] Player salió del rango.");
        }
    }
}
