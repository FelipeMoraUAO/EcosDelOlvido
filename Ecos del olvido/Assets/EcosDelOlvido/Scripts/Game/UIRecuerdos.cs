using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRecuerdos : MonoBehaviour
{
    public static UIRecuerdos Instance;

    public GameObject panelRecuerdo;   // El panel que se mostrará
    public Image imagenRecuerdo;       // Donde se coloca la imagen del recuerdo

    private void Awake()
    {
        Instance = this;
        panelRecuerdo.SetActive(false);
    }

    public void MostrarRecuerdo(Sprite sprite, float duracion = 10f)
    {
        imagenRecuerdo.sprite = sprite;
        panelRecuerdo.SetActive(true);
        StartCoroutine(EsconderRecuerdo(duracion));
    }

    IEnumerator EsconderRecuerdo(float t)
    {
        yield return new WaitForSeconds(t);
        panelRecuerdo.SetActive(false);
    }
}
