using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjetoTransicion : MonoBehaviour
{
    [Header("Configuración de interacción")]
    public string escenaDestino = "5Castillo";
    public int hechizosNecesarios = 2;

    [Header("UI")]
    public TextMeshProUGUI textoInteraccion; // "Presiona E para avanzar"
    public TextMeshProUGUI textoAviso;       // Aviso temporal

    private bool enRango = false;
    private LoaderScenes loader;

    void Start()
    {
        if (textoInteraccion != null) textoInteraccion.gameObject.SetActive(false);
        if (textoAviso != null) textoAviso.gameObject.SetActive(false);

        loader = Object.FindFirstObjectByType<LoaderScenes>();
    }

    void Update()
    {
        if (!enRango)
        {
            if (textoInteraccion != null && textoInteraccion.gameObject.activeSelf)
                textoInteraccion.gameObject.SetActive(false);
            return;
        }

        // Mostrar texto
        if (textoInteraccion != null)
            textoInteraccion.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
            VerificarYTransicionar();
    }

    void VerificarYTransicionar()
    {
        int cantidadHechizos = GameManager.Instance.hechizosRecolectados.Count;

        if (cantidadHechizos >= hechizosNecesarios)
        {
            if (loader != null)
                loader.Castillo();
            else
                SceneManager.LoadScene(escenaDestino);
        }
        else
        {
            if (textoAviso != null)
                StartCoroutine(MostrarAvisoTemporal($"Aún te faltan {hechizosNecesarios - cantidadHechizos} hechizos..."));
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
