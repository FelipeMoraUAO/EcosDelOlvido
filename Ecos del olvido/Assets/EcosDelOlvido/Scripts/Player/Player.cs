using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Atributos de Vida")]
    public int vidaMaxima = 100;
    public int vida;

    [Header("Configuraciones de daño")]
    public bool estaMuerto = false;

    void Start()
    {
        // Inicia con la vida completa
        vida = vidaMaxima;
    }

    // Método para recibir daño
    public void TomarDaño(int cantidad)
    {
        if (estaMuerto) return;

        vida -= cantidad;
        vida = Mathf.Clamp(vida, 0, vidaMaxima);

        if (vida <= 0)
        {
            Morir();
        }
    }

    // Método para curarse (opcional)
    public void Curar(int cantidad)
    {
        if (estaMuerto) return;

        vida += cantidad;
        vida = Mathf.Clamp(vida, 0, vidaMaxima);
    }

    // Método cuando la vida llega a 0
    void Morir()
    {
        estaMuerto = true;
        Debug.Log("💀 El jugador ha muerto");

        // Desactivar movimiento
        GetComponent<PlayerMove>().enabled = false;

        // Cargar escena derrota
        SceneManager.LoadScene("6Derrota");
    }
}
