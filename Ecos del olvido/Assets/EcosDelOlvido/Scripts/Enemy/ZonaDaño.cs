using UnityEngine;

public class ZonaDaño : MonoBehaviour
{
    // Cantidad de daño que la zona aplicará al jugador en cada ciclo
    public int daño = 5;

    // Intervalo de tiempo en segundos entre cada aplicación de daño
    public float intervaloDaño = 1f;

    // Indica si el jugador está dentro del área de daño
    private bool dentro = false;

    // Contador utilizado para controlar el tiempo entre daños
    private float contador = 0f;

    // Referencia al script Player que recibirá el daño
    private Player player;

    // Se ejecuta cuando un objeto entra en el collider con isTrigger activado
    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            dentro = true;
            player = other.GetComponent<Player>();

            // Reinicia el contador para que aplique daño inmediato al entrar
            contador = intervaloDaño;
        }
    }

    // Se ejecuta cuando un objeto sale del collider de la zona de daño
    void OnTriggerExit(Collider other)
    {
        // Verifica si el que salió es el jugador
        if (other.CompareTag("Player"))
        {
            dentro = false;
            player = null;
        }
    }

    // Se ejecuta una vez por frame
    void Update()
    {
        // Solo aplica daño si el jugador está dentro del área
        if (dentro && player != null)
        {
            // Aumenta el contador con el tiempo transcurrido
            contador += Time.deltaTime;

            // Si se cumple el intervalo, aplica daño
            if (contador >= intervaloDaño)
            {
                player.TomarDaño(daño);
                contador = 0f;
            }
        }
    }
}
