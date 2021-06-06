﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit {

    public static bool isGround, isMove;
    private float speedX;
    private new Collider2D collider;

    private bool isMode = true;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    public static bool IsGround { get { return isGround; } }
    public static bool IsMove { get { return isMove; } }

    private FootstepsCharacter foot;
    private string tagName;
    public float jumpDistance = 10f;

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
        foot = GetComponent<FootstepsCharacter>();
    }

    private void FixedUpdate() {
        animator.SetBool("inMove", false);
        animator.SetBool("isGround", isGround);
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        Collider2D[] coll = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, whatIsGround);

        foreach (Collider2D col in coll) {
            if (col.gameObject.tag == "Leaves")
                tagName = col.gameObject.tag;
        }
        Run();
    }

    private void Update() {
        Jump();
        BattleMode();
       
        GetStep();
    }


    private void Run() {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
            animator.SetBool("inMove", true);
            transform.Translate(speedX, 0, 0);
            isMove = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
            animator.SetBool("inMove", true);
            transform.Translate(-speedX, 0, 0);
            isMove = true;
        }

        else isMove = false;
    }

    private bool GetTag() // проверяем, есть ли коллайдер под ногами
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, jumpDistance, whatIsGround))
        {
            Debug.Log("whatIsGround");
            tagName = hit.transform.tag; // берем тег поверхности
            return true;
        }

        tagName = string.Empty;
        return false;
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

    void GetStep() 
    {
        switch (tagName)
        {
            case "Leaves":
                foot.PlayStep(FootstepsCharacter.StepsOn.Leaves, 1);
                break;
            case "Wood":
                foot.PlayStep(FootstepsCharacter.StepsOn.Wood, 1);
                break;
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
