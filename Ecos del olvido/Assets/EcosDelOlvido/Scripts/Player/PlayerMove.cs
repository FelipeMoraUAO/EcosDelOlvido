using UnityEngine;
using UnityEngine.InputSystem; // Requiere el nuevo Input System

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Física")]
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isRunning;
    private bool jumpInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // evita que se caiga al chocar
    }

    // =======================
    // NUEVO INPUT SYSTEM
    // =======================
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        isRunning = ctx.ReadValueAsButton();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            jumpInput = true;
    }

    private void FixedUpdate()
    {
        // --- DETECCIÓN DE SUELO ---
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        // --- MOVIMIENTO ---
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        if (moveDir.magnitude >= 0.1f)
        {
            float targetSpeed = isRunning ? runSpeed : walkSpeed;
            Vector3 move = transform.TransformDirection(moveDir) * targetSpeed;
            rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
        }

        // --- SALTO ---
        if (jumpInput && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
        jumpInput = false;
    }
}
