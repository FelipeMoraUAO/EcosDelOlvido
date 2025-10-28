using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public Image rellenoBarra;
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        float porcentaje = (float)player.vida / player.vidaMaxima;
        rellenoBarra.fillAmount = Mathf.Clamp01(porcentaje);
    }
}
