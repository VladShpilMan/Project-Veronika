using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Squirrel : Unit {
    [SerializeField] private Transform[] points;
    private bool onWay;
    private bool isIdle = false;
    private bool nextPoint = true;
    private int randPoint;
    private int temp;

    private float nextAttackTime = 0F;

    private void Start() {
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        
    }

    private void FixedUpdate() {

        RouteMovement();       
    }

    private void RouteMovement() {
        animator.SetBool("isMove", true);
        FlipX();
        if (nextPoint && !isIdle) {
            while (true) {
                temp = Random.Range(0, points.Length);
                if (randPoint != temp) {
                    randPoint = temp;
                    break;
                }
            }
            nextPoint = false;
            onWay = true;
        }

        if (onWay && !isIdle)
            transform.position = Vector2.MoveTowards(transform.position, points[randPoint].position, 0.05f);

        if ((transform.position.x == points[randPoint].position.x) && !isIdle)
        {
            nextPoint = true;
            onWay = false;
            isIdle = true;
            nextAttackTime = Time.time + Random.Range(1, 4); 
        }

        if (isIdle) Idel();
    }

    private void FlipX() {
        if(transform.position.x > points[randPoint].position.x)
            sprite.flipX = true;
        else 
            sprite.flipX = false;
    }

    private void Idel() {
        if (Time.time <= nextAttackTime)
        {
            animator.SetInteger("number", Random.Range(1, 3));
            animator.SetBool("isMove", false);
        }
        else isIdle = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //animator.SetBool("isDie", false);

        if (currentHealth <= 0)
            Die();
    }
    private void Die()
    {
        Debug.Log("Die");
        animator.SetBool("isDie", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
