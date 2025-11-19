using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 7;              // Velocidad de movimiento hacia adelante/atrás
    public float rotationSpeed = 250;       // Velocidad de rotación del personaje

    public Animator animator;               // Controlador de animaciones del jugador

    private float x, y;                     // Valores de entrada horizontal y vertical

    public Rigidbody rb;                    // Rigidbody del jugador para físicas
    public float jumpHeight = 3;            // Fuerza aplicada en el salto

    public Transform groundCheck;           // Punto que verifica si el jugador toca el suelo
    public float groundDistance = 0.1f;     // Radio de detección del suelo
    public LayerMask groundMask;            // Capas que se consideran "suelo"

    bool isGrounded;                        // Indica si el jugador está en el suelo

    void Update()
    {
        // Captura entradas del jugador
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Rotación del jugador
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);

        // Movimiento hacia adelante/atrás
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        // Actualiza parámetros del animator
        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);

        // Ejecuta animación de ataque
        if (Input.GetKey("f"))
        {
            animator.Play("Attack");
        }

        // Activa estado de movimiento en animaciones
        if (x != 0 || y != 0)
        {
            animator.SetBool("Other", true);
        }

        // Verifica si está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Ejecuta salto si está en el suelo
        if (Input.GetKey("space") && isGrounded)
        {
            animator.Play("Jump");
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
