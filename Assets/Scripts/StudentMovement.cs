using UnityEngine;
using UnityEngine.InputSystem;

public class StudentMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashCooldown = 1f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Input
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction interactAction;

    // Movement
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    // Animator parameters
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsDashing = Animator.StringToHash("IsDashing");

    private void Awake()
    {
        // Get components if not assigned
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize input
        InitializeInput();
    }

    private void InitializeInput()
    {
        // Get PlayerInput component
        playerInput = GetComponent<PlayerInput>();

        if (playerInput == null)
        {
            playerInput = gameObject.AddComponent<PlayerInput>();
            Debug.LogWarning("PlayerInput component added automatically. Please set up Input Actions Asset.");
        }

        playerInput.defaultActionMap = "Student";
    }

    private void OnEnable()
    {
        // Enable all actions
        moveAction?.Enable();
        dashAction?.Enable();
        interactAction?.Enable();
    }

    private void OnDisable()
    {
        // Disable all actions
        moveAction?.Disable();
        dashAction?.Disable();
        interactAction?.Disable();
    }

    private void Update()
    {
        // Update animations
        UpdateAnimations();

        // Flip sprite based on movement direction
        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        // Calculate target velocity
        Vector2 targetVelocity = moveInput * moveSpeed;

        // Smooth movement using acceleration/deceleration
        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            targetVelocity,
            (moveInput.magnitude > 0.1f ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        // Apply velocity
        rb.velocity = currentVelocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    /*public void OnDash(InputAction.CallbackContext context)
    {
        if (Time.time < lastDashTime + dashCooldown) return;
        if (moveInput.magnitude < 0.1f) return; // Can't dash without direction

        StartCoroutine(DashCoroutine());
    }

    private System.Collections.IEnumerator DashCoroutine()
    {
        isDashing = true;
        lastDashTime = Time.time;

        // Store dash direction
        Vector2 dashDirection = moveInput.normalized;

        // Apply dash force
        rb.velocity = dashDirection * dashForce;

        // Update animation
        if (animator != null)
            animator.SetBool(IsDashing, true);

        // Dash duration
        yield return new WaitForSeconds(0.2f);

        // End dash
        isDashing = false;

        if (animator != null)
            animator.SetBool(IsDashing, false);
    }*/

    private void OnInteract(InputAction.CallbackContext context)
    {
        // Handle interaction (e.g., talking to NPCs, picking up items)
        Debug.Log("Interact pressed!");

        // You can add raycast or collision check here
        // CheckForInteractables();
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;

        bool isMoving = moveInput.magnitude > 0.1f && !isDashing;
        animator.SetBool(IsMoving, isMoving);
    }

    // Public method to disable/enable movement (for cutscenes, etc.)
    public void SetMovementEnabled(bool enabled)
    {
        if (!enabled)
        {
            rb.velocity = Vector2.zero;
            currentVelocity = Vector2.zero;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        if (dashAction != null)
            //dashAction.performed -= OnDash;

        if (interactAction != null)
            interactAction.performed -= OnInteract;
    }
}