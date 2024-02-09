using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public string horizontalInputAxis = "Horizontal";
    public string verticalInputAxis = "Vertical";

    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isGrounded = false; // Track the grounded state

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis(horizontalInputAxis);

        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Flip the character based on the input direction
        FlipCharacter(horizontalInput);

        // Check for jump input
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            // Ensure the velocity is zero before applying the impulse
            rb.velocity = Vector2.zero;

            // Apply a vertical impulse to simulate a jump
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            isGrounded = false; // Set grounded state to false
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set grounded state to true when colliding with ground
        }
    }

    private void FlipCharacter(float horizontalInput)
    {
        // Flip the character sprite based on the input direction
        if (horizontalInput < 0)
        {
            // If moving left, flip the character to face left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0)
        {
            // If moving right, keep the character facing right
            transform.localScale = new Vector3(1, 1, 1);
        }
        // If horizontalInput is 0, maintain the current facing direction
    }
}
