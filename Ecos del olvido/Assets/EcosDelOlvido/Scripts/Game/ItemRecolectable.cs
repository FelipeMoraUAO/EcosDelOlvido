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

    void Update()
    {
        // Solo se activa si el jugador está cerca y presiona E
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            switch (tipoItem)
            {
                case TipoItem.Hechizo:
                    Debug.Log("Hechizo recolectado: " + nombreItem);
                    GameManager.Instance.AgregarHechizo(nombreItem);
                    break;

                case TipoItem.Recuerdo:
                    Debug.Log("Recuerdo recolectado: " + nombreItem);
                    GameManager.Instance.AgregarRecuerdo(nombreItem);
                    break;
            }

            // Desactivar el objeto tras recolectarlo
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador entró en el área de recolección de " + nombreItem);
            enRango = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador salió del área de recolección de " + nombreItem);
            enRango = false;
        }
    }
}
