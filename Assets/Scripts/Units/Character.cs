using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{

    private float speedX;
    private new Collider2D collider;


    private bool isGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;



    protected void Awake() {
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        speedX = speed / 100;
        collider = GetComponentInChildren<Collider2D>();
    }

    private void FixedUpdate() {
        animator.SetBool("inMove", false);
        animator.SetBool("isGround", isGround);

        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        Run();
    }

    private void Update() {
        Jump();
    }

    private void Run() {

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
            animator.SetBool("inMove", true);
            transform.Translate(speedX, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
            animator.SetBool("inMove", true);
            transform.Translate(-speedX, 0, 0);
        }
    }

    private void Jump() {
        if(Input.GetButtonDown("Jump") && isGround)
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
