using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public bool canMove = true;
    public bool isGrounded;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
   

    private Rigidbody2D rb;
    private Animator animator;
  


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {

            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

          //  animator.SetFloat("Speed", Mathf.Abs(moveInput));

            if (moveInput != 0)
            {
                transform.localScale = new Vector3(moveInput, 1, 1);
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        //  animator.SetBool("IsGrounded", isGrounded);
        // animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }
}