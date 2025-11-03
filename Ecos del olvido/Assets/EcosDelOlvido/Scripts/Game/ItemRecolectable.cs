using UnityEngine;

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
    private Transform jugador;
    private Inventario inventario; // referencia al UI

    void Start()
    {
        // Buscar referencias al jugador y al inventario
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;

        // ✅ Método actualizado recomendado por Unity (más rápido y moderno)
        inventario = Object.FindFirstObjectByType<Inventario>();
    }

    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            switch (tipoItem)
            {
                case TipoItem.Hechizo:
                    Debug.Log("✅ Recolectando (Hechizo): " + nombreItem);
                    GameManager.Instance.AgregarHechizo(nombreItem);
                    break;

                case TipoItem.Recuerdo:
                    Debug.Log("✅ Recolectando (Recuerdo): " + nombreItem);
                    GameManager.Instance.AgregarRecuerdo(nombreItem);
                    break;
            }

            // 🔹 Actualiza el contador del UI
            if (inventario != null)
                inventario.RecolectarObjeto();

            // 🔹 Desactiva el objeto tras recolectarlo
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🧙 Jugador entró al rango del ítem: " + nombreItem);
            enRango = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🚶 Jugador salió del rango del ítem: " + nombreItem);
            enRango = false;
        }
    }
}
