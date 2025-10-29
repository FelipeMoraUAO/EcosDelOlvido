using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public Image rellenoBarra;
    private Player player;

    void Start()
    {
        // Intenta encontrar al jugador Caballero
        GameObject playerObj = GameObject.Find("PlayerCaballero");

        if (playerObj != null)
        {
            player = playerObj.GetComponent<Player>();
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró el objeto 'PlayerCaballero'. Este script solo se usa con ese personaje.");
        }
    }

    void Update()
    {
        // Solo ejecuta si hay un jugador con vida
        if (player == null || rellenoBarra == null)
            return;

        float porcentaje = (float)player.vida / player.vidaMaxima;
        rellenoBarra.fillAmount = Mathf.Clamp01(porcentaje);
    }
}
