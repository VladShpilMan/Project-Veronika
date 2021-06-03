using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

    protected bool movingRight;
    protected bool chill = false;
    protected bool angry = false;
    protected bool goBack = false;
    protected Transform character;

    [SerializeField] protected float speedAnim;
    [SerializeField] protected float attackRate;
    protected float nextAttackTime;
    protected float speedAtMoment;

   

    protected virtual void Start() {
        GetReferences();

        speedAtMoment = (float)speed / 100;
        currentHealth = maxHealth;
    }

    protected virtual void GetReferences() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        character = GameObject.FindGameObjectWithTag("Character").transform;
    }

    public virtual void TakeDamage(int damage) { }

    protected void Die()
    {
        Debug.Log("Die");
        animator.SetTrigger("IsDie");
        animator.SetBool("isDie", true);
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
