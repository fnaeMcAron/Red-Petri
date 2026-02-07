using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.Rendering.Universal;

public class StudentMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public float wallWalkSpeed = 4f;
    public Light2D light;

    Rigidbody2D rb;
    bool grounded = true;
    Vector2 moveVec;

    [HideInInspector] public bool airControl; // 100
    [HideInInspector] public bool canWallWalk; // 010
    [HideInInspector] public bool canDash; // 110
    [HideInInspector] public bool canActivateElectric; //001
    [HideInInspector] public bool lightEnabled; // 101
    [HideInInspector] public bool canChangeSize; // 011
    public int jumpCount;
    [HideInInspector] public bool extraJump; // 111

    bool roomAllowsWallWalk;
    bool WallWalkMode => canWallWalk && roomAllowsWallWalk;

    float baseSpeed;
    float baseJumpForce;
    Vector3 baseScale;
    float baseGravity;
    int jumpRemains;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        baseSpeed = speed;
        baseJumpForce = jumpForce;
        baseScale = transform.localScale;
        baseGravity = rb.gravityScale;
    }

    void FixedUpdate()
    {
        if (WallWalkMode)
        {
            rb.velocity = moveVec * wallWalkSpeed;
            return;
        }

        if (grounded || airControl)
        {
            rb.velocity = new Vector2(moveVec.x * speed, rb.velocity.y);
        }
    }
    void Update()
    {
        light.enabled = lightEnabled;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        grounded = true;
        jumpRemains = jumpCount;
    }

    void UpdateWallWalkState()
    {
        rb.gravityScale = WallWalkMode ? 0 : baseGravity;
    }

    public void SetRoomSpace(bool allow)
    {
        roomAllowsWallWalk = allow;
        UpdateWallWalkState();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveVec = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (canWallWalk && roomAllowsWallWalk) return;

        if (grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            grounded = false;
        }
        else if (extraJump && jumpRemains > -1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRemains--;
        }
    }

    public void ResetModifiers()
    {
        speed = baseSpeed;
        jumpForce = baseJumpForce;
        transform.localScale = baseScale;

        airControl = false;
        canWallWalk = false;
        canDash = false;
        extraJump = false;
        canChangeSize = false;
        canActivateElectric = false;
        lightEnabled = false;

        UpdateWallWalkState();
    }
}