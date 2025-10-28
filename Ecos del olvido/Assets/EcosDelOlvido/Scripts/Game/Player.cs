using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Atributos del jugador")]
    public float velocidad = 5f;
    public int vida = 3;
    public int vidaMaxima = 3;
    public float fuerzaSalto = 7f;

    [Header("Detección de suelo")]
    public Transform sensorSuelo;
    public LayerMask capaSuelo;

    Rigidbody rb;
    bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        vida = vidaMaxima;
    }

    void Update()
    {
        Mover();
        RevisarSuelo();

        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            Saltar();
        }
    }

    void Mover()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direccion = new Vector3(x, 0, z).normalized;
        Vector3 movimiento = transform.TransformDirection(direccion) * velocidad;
        rb.MovePosition(rb.position + movimiento * Time.deltaTime);
    }

    void RevisarSuelo()
    {
        enSuelo = Physics.CheckSphere(sensorSuelo.position, 0.2f, capaSuelo);
    }

    void Saltar()
    {
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            vida = 0;
            Debug.Log("Jugador muerto");
            // Aquí puedes cambiar de escena o reiniciar
        }
    }

    public void RecuperarVida(int cantidad)
    {
        vida = Mathf.Min(vida + cantidad, vidaMaxima);
    }
}
