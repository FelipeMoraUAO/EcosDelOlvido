using UnityEngine;

public class Recolectable : MonoBehaviour
{
    private Inventario inventario;

    void Start()
    {
        inventario = Object.FindFirstObjectByType<Inventario>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventario.RecolectarObjeto();
            Destroy(gameObject);
        }
    }
}
