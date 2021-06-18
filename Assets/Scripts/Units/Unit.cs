/*
    Base class for all units in the game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    protected SpriteRenderer sprite;
    protected Animator animator;
    protected new Rigidbody2D rigidbody;

    public virtual void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
            animator.SetBool("isDie", false);
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
