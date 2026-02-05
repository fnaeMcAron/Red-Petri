using UnityEngine;
using UnityEngine.InputSystem;

public class StudentMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    Rigidbody2D rb;
    float moveInput;
    bool grounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            grounded = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        grounded = true;
    }
}
