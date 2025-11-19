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
    private LoaderScenes loader;               // Referencia al sistema de carga de escenas personalizado

    // Inicializa referencias y oculta textos
    void Start()
    {
        if (textoInteraccion != null) textoInteraccion.gameObject.SetActive(false);
        if (textoAviso != null) textoAviso.gameObject.SetActive(false);

        loader = Object.FindFirstObjectByType<LoaderScenes>();
    }

    // Revisa si el jugador está en rango e interpreta la tecla E
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

        if (Input.GetKeyDown(KeyCode.E))
            VerificarYTransicionar();
    }

    // Verifica si el jugador tiene los hechizos requeridos y decide si avanzar o mostrar aviso
    void VerificarYTransicionar()
    {
        int cantidadHechizos = GameManager.Instance.hechizosRecolectados.Count;

        if (cantidadHechizos >= hechizosNecesarios)
        {
            if (loader != null)
                loader.Castillo();              // Usa transición personalizada
            else
                SceneManager.LoadScene(escenaDestino); // Carga directa como respaldo
        }
        else
        {
            if (textoAviso != null)
                StartCoroutine(MostrarAvisoTemporal(
                    $"Aún te faltan {hechizosNecesarios - cantidadHechizos} hechizos..."
                ));
        }
    }

    // Muestra un aviso por unos segundos y luego lo oculta
    System.Collections.IEnumerator MostrarAvisoTemporal(string mensaje)
    {
        textoAviso.text = mensaje;
        textoAviso.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        textoAviso.gameObject.SetActive(false);
    }

    // Detecta cuando el jugador entra en el trigger del objeto
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enRango = true;
    }

    // Detecta cuando el jugador sale del trigger del objeto
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enRango = false;
    }
}
