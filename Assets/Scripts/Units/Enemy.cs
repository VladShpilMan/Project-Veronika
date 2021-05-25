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

   

    public virtual void Start()
    {
        speedAtMoment = (float)speed / 100;
        rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        sprite = GetComponentInChildren<SpriteRenderer>();
        character = GameObject.FindGameObjectWithTag("Character").transform;
    }

    //public virtual void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //    //animator.SetTrigger("Hurt"); // play animation of taking damage
    //    //animator.SetFloat("speedAnimHurt", speedAnimHurt);

    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    //public virtual void Die()
    //{
    //    //animator.SetBool("isDead", true); // play animation of death

    //    GetComponent<CapsuleCollider2D>().enabled = false;
    //    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // set the physical parameter to static, so that the gameObject does not fall into the ground
    //    this.enabled = false; // disable script for gameObject
    //    //this.GetComponent<EnemyAttack>().enabled = false;
    //}
}
