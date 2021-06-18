/*
    Base class for all enemys in the game
*/
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
}
