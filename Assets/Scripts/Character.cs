using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit {

    private float speedX;


    private void  Awake() {
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        speedX = speed / 100;
    }

    private void Update()
    {
        animator.SetBool("inMove", false);

        Run();
    }

    private void Run() {

        

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
            animator.SetBool("inMove", true);
            transform.Translate(speedX, 0, 0);
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
            animator.SetBool("inMove", true);
            transform.Translate(-speedX, 0, 0);
        }
    }
}
