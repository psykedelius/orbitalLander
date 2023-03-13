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
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {

            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity     = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            isGrounded      = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
            //  animator.SetFloat("Speed", Mathf.Abs(moveInput));
            if (isGrounded)
            {
                animator.SetBool("isGrounded", true);
            }
            else
            {
                animator.SetBool("isGrounded", false);
            }
            if (moveInput != 0)
            {
                transform.localScale = new Vector3(moveInput, 1, 1);
                animator.SetBool("inputX", true);

            }
            else
            {
                animator.SetBool("inputX", false);
            }

           

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        //  animator.SetBool("IsGrounded", isGrounded);
        // animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }
}