using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Atributos de Vida")]
    public int vidaMaxima = 100;     // Vida total máxima del jugador
    public int vida;                 // Vida actual del jugador

    [Header("Configuraciones de daño")]
    public bool estaMuerto = false;  // Indica si el jugador ya ha muerto

    void Start()
    {
        // Inicializa la vida al valor máximo
        vida = vidaMaxima;
    }

    // Reduce la vida del jugador según el daño recibido
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

    // Aumenta la vida del jugador según la cantidad indicada
    public void Curar(int cantidad)
    {
        if (estaMuerto) return;

        vida += cantidad;
        vida = Mathf.Clamp(vida, 0, vidaMaxima);
    }

    // Se ejecuta cuando la vida llega a 0
    void Morir()
    {
        estaMuerto = true;

        // Desactiva el movimiento del jugador
        GetComponent<PlayerMove>().enabled = false;

        // Cambia a la escena de derrota
        SceneManager.LoadScene("6Derrota");
    }
}