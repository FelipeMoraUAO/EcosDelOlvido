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

    private bool enRango = false;
    private Inventario inventario;

    // Texto UI
    private TextMeshProUGUI textoRecolectar;

    void Start()
    {
        inventario = Object.FindFirstObjectByType<Inventario>();

        // Busca el texto UI por nombre
        textoRecolectar = GameObject.Find("TextoRecolectar")?.GetComponent<TextMeshProUGUI>();

        // Oculta el texto al inicio
        if (textoRecolectar != null)
            textoRecolectar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            // Recolectar según tipo
            if (tipoItem == TipoItem.Hechizo)
                GameManager.Instance.AgregarHechizo(nombreItem);
            else if (tipoItem == TipoItem.Recuerdo)
                GameManager.Instance.AgregarRecuerdo(nombreItem);

            // Actualiza UI del inventario
            if (inventario != null)
                inventario.RecolectarObjeto();

            // Oculta el aviso
            if (textoRecolectar != null)
                textoRecolectar.gameObject.SetActive(false);

            // Desaparece el ítem
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = true;

            // Mostrar texto
            if (textoRecolectar != null)
                textoRecolectar.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = false;

            // Ocultar texto
            if (textoRecolectar != null)
                textoRecolectar.gameObject.SetActive(false);
        }
    }
}
