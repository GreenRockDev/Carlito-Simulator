using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class chef : MonoBehaviour
{
    public float speed = 5f;
    public float stopDistance = 0.1f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Mouse.current == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        float distance = Vector2.Distance(mouseWorldPos, rb.position);
        
        if (distance > stopDistance)
        {
            Vector2 direction = (mouseWorldPos - rb.position).normalized;

            Vector2 targetPos = rb.position + direction * speed * Time.fixedDeltaTime;

            // Check if moving would overshoot the target
            if (Vector2.Distance(targetPos, mouseWorldPos) < stopDistance)
            {
                rb.MovePosition(mouseWorldPos); // Snap to mouse to avoid overshoot
            }
            else
            {
                rb.MovePosition(targetPos);
            }

            // Rotate toward mouse
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Just in case physics wants to push slightly
        }
    }
}
