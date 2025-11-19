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

    [Header("UI")]
    public TextMeshProUGUI textoRecolectar;  // ← Ahora asignado por Inspector

    private bool enRango = false;
    private Inventario inventario;

    void Start()
    {
        inventario = Object.FindFirstObjectByType<Inventario>();

        if (textoRecolectar != null)
            textoRecolectar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            // Recolectar
            if (tipoItem == TipoItem.Hechizo)
                GameManager.Instance.AgregarHechizo(nombreItem);
            else
                GameManager.Instance.AgregarRecuerdo(nombreItem);

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
