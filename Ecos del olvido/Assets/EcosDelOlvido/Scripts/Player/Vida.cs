using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public Image rellenoBarra;        // Imagen UI que representa la barra de vida
    private Player player;            // Referencia al componente Player del jugador

    void Start()
    {
        // Busca el GameObject del jugador llamado "PlayerCaballero"
        GameObject playerObj = GameObject.Find("PlayerCaballero");

        if (playerObj != null)
        {
            player = playerObj.GetComponent<Player>();  // Obtiene el componente Player
        }
        else
        {
            // Advertencia si el jugador no existe en la escena
            Debug.LogWarning("No se encontró el objeto 'PlayerCaballero'. Este script solo se usa con ese personaje.");
        }
    }

    void Update()
    {
        // Evita errores si la referencia a Player o la barra no existe
        if (player == null || rellenoBarra == null)
            return;

        // Calcula la proporción de vida entre 0 y 1
        float porcentaje = (float)player.vida / player.vidaMaxima;

        // Actualiza el valor visual de la barra de vida
        rellenoBarra.fillAmount = Mathf.Clamp01(porcentaje);
    }
}
