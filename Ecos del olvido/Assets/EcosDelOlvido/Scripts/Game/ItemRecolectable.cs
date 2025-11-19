using UnityEngine;
using TMPro;

public enum TipoItem
{
    Hechizo,
    Recuerdo
}

public class ItemRecolectable : MonoBehaviour
{
    public TipoItem tipoItem;              // Tipo de objeto (Hechizo o Recuerdo)
    public string nombreItem;              // Nombre del ítem para el GameManager

    [Header("Imagen del recuerdo (solo si es Recuerdo)")]
    public Sprite imagenDelRecuerdo;       // Imagen que se mostrará al recolectar un recuerdo

    [Header("UI")]
    public TextMeshProUGUI textoRecolectar; // Texto que indica "Presiona E para recolectar"

    private bool enRango = false;          // Indica si el jugador está dentro del rango de recolección
    private Inventario inventario;         // Referencia al inventario en la escena

    // Inicializa referencias y oculta el texto de interacción
    void Start()
    {
        inventario = Object.FindFirstObjectByType<Inventario>();

        // Si no fue asignado por inspector, lo busca automáticamente
        if (textoRecolectar == null)
            textoRecolectar = GameObject.Find("TextoRecolectar")?.GetComponent<TextMeshProUGUI>();

        if (textoRecolectar != null)
            textoRecolectar.gameObject.SetActive(false);
    }

    // Detecta si se presiona E mientras el jugador está en rango
    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            // Procesa según tipo de ítem
            if (tipoItem == TipoItem.Hechizo)
            {
                GameManager.Instance.AgregarHechizo(nombreItem);
            }
            else if (tipoItem == TipoItem.Recuerdo)
            {
                GameManager.Instance.AgregarRecuerdo(nombreItem);

                // Muestra la imagen del recuerdo si existe un controlador UI
                if (UIRecuerdos.Instance != null && imagenDelRecuerdo != null)
                {
                    UIRecuerdos.Instance.MostrarRecuerdo(imagenDelRecuerdo);
                }
            }

            // Actualiza contador de inventario
            if (inventario != null)
                inventario.RecolectarObjeto();

            // Oculta el texto de interacción
            if (textoRecolectar != null)
                textoRecolectar.gameObject.SetActive(false);

            // Oculta el objeto recolectado
            gameObject.SetActive(false);
        }
    }

    // Detecta cuando el jugador entra en la zona de recolección
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        enRango = true;

        if (textoRecolectar != null)
            textoRecolectar.gameObject.SetActive(true);
    }

    // Detecta cuando el jugador sale de la zona de recolección
    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        enRango = false;

        if (textoRecolectar != null)
            textoRecolectar.gameObject.SetActive(false);
    }
}
