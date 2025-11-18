using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjetoTransicion : MonoBehaviour
{
    [Header("Configuración de interacción")]
    public string escenaDestino = "5Castillo";  // Escena destino
    public int hechizosNecesarios = 2;          // Cuántos hechizos se requieren

    [Header("Referencias UI")]
    public TextMeshProUGUI textoInteraccion;    // Texto: "Presiona E para interactuar"
    public TextMeshProUGUI textoAviso;          // Texto: "Falta recolectar..."

    private bool enRango = false;
    private LoaderScenes loader;

    void Start()
    {
        // Oculta los textos al inicio
        if (textoInteraccion != null) textoInteraccion.gameObject.SetActive(false);
        if (textoAviso != null) textoAviso.gameObject.SetActive(false);

        // Busca el LoaderScenes activo en la escena
        loader = Object.FindFirstObjectByType<LoaderScenes>();
        if (loader == null)
        {
            Debug.LogWarning("⚠️ No se encontró LoaderScenes en la escena. La transición usará SceneManager directamente.");
        }
    }

    void Update()
    {
        if (enRango)
        {
            if (textoInteraccion != null && !textoInteraccion.gameObject.activeSelf)
                textoInteraccion.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                VerificarYTransicionar();
        }
        else
        {
            if (textoInteraccion != null && textoInteraccion.gameObject.activeSelf)
                textoInteraccion.gameObject.SetActive(false);
        }
    }

    void VerificarYTransicionar()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("No se encontró GameManager en la escena.");
            return;
        }

        int cantidadHechizos = GameManager.Instance.hechizosRecolectados.Count;

        if (cantidadHechizos >= hechizosNecesarios)
        {
            Debug.Log("Tienes todos los hechizos. Transicionando a " + escenaDestino + "...");

            if (loader != null)
                loader.Castillo(); // Usa tu método personalizado
            else
                SceneManager.LoadScene(escenaDestino); // Fallback

        }
        else
        {
            Debug.Log("Aún te faltan hechizos por recolectar.");
            if (textoAviso != null)
                StartCoroutine(MostrarAvisoTemporal($"Aún te falta recolectar los {hechizosNecesarios} hechizos..."));
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
            enRango = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enRango = false;
    }
}
