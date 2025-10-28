using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Inventario : MonoBehaviour
{
    [Header("Configuración del inventario")]
    private string nombreObjeto;
    private int totalObjetos;
    private int objetosRecolectados = 0;

    [Header("Referencia UI")]
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoContador;

    void Start()
    {
        // Detecta la escena actual y configura automáticamente los textos
        string escenaActual = SceneManager.GetActiveScene().name;

        if (escenaActual == "4Santuario")
        {
            nombreObjeto = "Hechizo Antiguo";
            totalObjetos = 2;
        }
        else if (escenaActual == "5Castillo")
        {
            nombreObjeto = "Memory Fragment";
            totalObjetos = 4;
        }

        ActualizarUI();
    }

    public void RecolectarObjeto()
    {
        objetosRecolectados++;

        if (objetosRecolectados > totalObjetos)
            objetosRecolectados = totalObjetos;

        ActualizarUI();
    }

    private void ActualizarUI()
    {
        if (textoNombre != null)
            textoNombre.text = nombreObjeto;

        if (textoContador != null)
            textoContador.text = $"{objetosRecolectados}/{totalObjetos}";
    }

    public int ObtenerObjetosRecolectados()
    {
        return objetosRecolectados;
    }
}
