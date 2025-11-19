using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRecuerdos : MonoBehaviour
{
    public static UIRecuerdos Instance; // Instancia global del sistema de recuerdos

    public GameObject panelRecuerdo;   // Panel donde se muestra el recuerdo
    public Image imagenRecuerdo;       // Imagen que se actualizará con el recuerdo mostrado

    private void Awake()
    {
        // Inicializa la instancia y oculta el panel al comenzar
        Instance = this;
        panelRecuerdo.SetActive(false);
    }

    public void MostrarRecuerdo(Sprite sprite, float duracion = 10f)
    {
        // Muestra un recuerdo en pantalla por una duración determinada
        imagenRecuerdo.sprite = sprite;
        panelRecuerdo.SetActive(true);
        StartCoroutine(EsconderRecuerdo(duracion));
    }

    IEnumerator EsconderRecuerdo(float t)
    {
        // Oculta el panel luego del tiempo indicado
        yield return new WaitForSeconds(t);
        panelRecuerdo.SetActive(false);
    }
}
