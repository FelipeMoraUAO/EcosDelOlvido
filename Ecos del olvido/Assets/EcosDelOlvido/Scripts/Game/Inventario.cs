using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Inventario : MonoBehaviour
{
    [Header("Configuración del inventario")]
    private string nombreObjeto;       // Nombre del tipo de objeto que se recolecta en la escena
    private int totalObjetos;          // Cantidad total de objetos requeridos en la escena
    private int objetosRecolectados = 0; // Cantidad actual de objetos recolectados

    [Header("Referencia UI")]
    public TextMeshProUGUI textoNombre;   // Texto que muestra el nombre del objeto
    public TextMeshProUGUI textoContador; // Texto que muestra el progreso de recolección

    // Configura el inventario según la escena actual y actualiza la UI
    void Start()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        // Asigna nombre y cantidad según la escena
        if (escenaActual == "4Santuario")
        {
            nombreObjeto = "Hechizo Antiguo";
            totalObjetos = 2;
        }
        else if (escenaActual == "5Castillo")
        {
            nombreObjeto = "Memory Fragment";
            totalObjetos = 5;
        }

        ActualizarUI();
    }

    // Incrementa la cantidad de objetos recolectados y actualiza la UI
    public void RecolectarObjeto()
    {
        objetosRecolectados++;

        // Evita superar el límite máximo
        if (objetosRecolectados > totalObjetos)
            objetosRecolectados = totalObjetos;

        ActualizarUI();
    }

    // Actualiza el nombre del objeto y el contador en pantalla
    private void ActualizarUI()
    {
        if (textoNombre != null)
            textoNombre.text = nombreObjeto;

        if (textoContador != null)
            textoContador.text = $"{objetosRecolectados}/{totalObjetos}";
    }

    // Devuelve la cantidad de objetos recolectados
    public int ObtenerObjetosRecolectados()
    {
        return objetosRecolectados;
    }
}
