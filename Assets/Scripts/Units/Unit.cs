/*
    Base class for all units in the game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    protected bool movingRight;
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    protected SpriteRenderer sprite;
    protected Animator animator;
    protected new Rigidbody2D rigidbody;

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float checkRadius;
    [SerializeField] protected LayerMask whatIsGround;
    protected bool isGround;

    protected void CheckGround()
    {
        animator.SetBool("isGround", isGround);
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    public virtual void TakeDamage(int damage, float repulsion) {
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
            animator.SetBool("isDie", false);
            rigidbody.AddForce(transform.up * 1.5F, ForceMode2D.Impulse);
            if(repulsion != 0) rigidbody.AddForce(transform.right * repulsion, ForceMode2D.Impulse);
        }
        else Die();
    }

    protected virtual void Die() {
        Debug.Log("Die " + this.gameObject);
        animator.SetBool("isDie", true);
        animator.SetTrigger("Hit");
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
