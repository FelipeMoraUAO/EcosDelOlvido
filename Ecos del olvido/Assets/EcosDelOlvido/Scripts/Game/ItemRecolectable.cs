using UnityEngine;
using TMPro;

public enum TipoItem
{
    Hechizo,
    Recuerdo
}

public class ItemRecolectable : MonoBehaviour
{
    public TipoItem tipoItem;
    public string nombreItem;

    [Header("Imagen del recuerdo (solo si es Recuerdo)")]
    public Sprite imagenDelRecuerdo;

    [Header("UI")]
    public TextMeshProUGUI textoRecolectar;  // Asignar por inspector (opcional)

    private bool enRango = false;
    private Inventario inventario;

    void Start()
    {
        inventario = Object.FindFirstObjectByType<Inventario>();

        // Si no asignaste el TMP por inspector, lo intentamos buscar por nombre (fallback)
        if (textoRecolectar == null)
            textoRecolectar = GameObject.Find("TextoRecolectar")?.GetComponent<TextMeshProUGUI>();

        if (textoRecolectar != null)
            textoRecolectar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            if (tipoItem == TipoItem.Hechizo)
            {
                GameManager.Instance.AgregarHechizo(nombreItem);
            }
            else if (tipoItem == TipoItem.Recuerdo)
            {
                GameManager.Instance.AgregarRecuerdo(nombreItem);

                // Mostrar imagen del recuerdo en el UI (si existe el controlador)
                if (UIRecuerdos.Instance != null && imagenDelRecuerdo != null)
                {
                    UIRecuerdos.Instance.MostrarRecuerdo(imagenDelRecuerdo);
                }
            }

            if (inventario != null)
                inventario.RecolectarObjeto();

            if (textoRecolectar != null)
                textoRecolectar.gameObject.SetActive(false);

            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        enRango = true;
        if (textoRecolectar != null)
            textoRecolectar.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        enRango = false;
        if (textoRecolectar != null)
            textoRecolectar.gameObject.SetActive(false);
    }
}
