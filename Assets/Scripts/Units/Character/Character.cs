using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit {

    public bool isGround, isMove;
    private float speedX;
    private new Collider2D collider;

    private bool isMode = true;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    public bool IsGround { get { return isGround; } }
    public bool IsMove { get { return isMove; } }

    protected void Awake() {
        GetReferences();

        currentHealth = maxHealth;
        speedX = speed / 100;
    }

    private void GetReferences() {
        collider = GetComponentInChildren<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        animator.SetBool("inMove", false);
        animator.SetBool("isGround", isGround);
        isMove = false;

        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        Run();      
    }

    private void Update() {
        Jump();
        BattleMode();
    }


    private void Run() {
        isMove = true;
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

    void BattleMode()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (isMode)
            {
                animator.SetBool("isBattleMode", isMode);
                animator.SetFloat("speed", 1.0f);
                isMode = !isMode;
            }
            else
            {
                animator.SetBool("isBattleMode", isMode);
                animator.SetFloat("speed", -1.0f);
                isMode = !isMode;
            }        
    }

    public void TakeDamage(int damage) {

        currentHealth -= damage;
        Debug.Log("Hit");
        if (currentHealth > 0) {
            animator.SetTrigger("Hit");
            animator.SetBool("isDie", false);
        }
        else Die();
    }

    private void Die() {
        animator.SetBool("isDie", true);
        animator.SetTrigger("Hit");
    }
}
