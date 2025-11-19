using UnityEngine;

public class ZonaDaño : MonoBehaviour
{
    public int daño = 5;              // Daño por tick
    public float intervaloDaño = 1f;  // Cada cuántos segundos hace daño

    private bool dentro = false;
    private float contador = 0f;
    private Player player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dentro = true;
            player = other.GetComponent<Player>();
            contador = intervaloDaño; // daño inmediato al entrar
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dentro = false;
            player = null;
        }
    }

    void Update()
    {
        if (dentro && player != null)
        {
            contador += Time.deltaTime;

            if (contador >= intervaloDaño)
            {
                player.TomarDaño(daño);
                contador = 0f;
            }
        }
    }
}
