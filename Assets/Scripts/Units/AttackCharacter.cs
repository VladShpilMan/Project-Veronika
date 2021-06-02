﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharacter : MonoBehaviour
{
    private bool isCombatMode = false;
    private Animator animator;
    private float nextAttackTime = 0F;
    private float attackRate = 3f;
    [SerializeField] private int attackDamage;

    [SerializeField]private Transform attackPoint;
    [SerializeField]private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    private SpriteRenderer sprite;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() {
        animator.SetBool("isAttack", false);

        EnteringCombatMode();
        Attack();
        FlipGizmos();
    }


    private void EnteringCombatMode() {
        if (Input.GetKeyDown(KeyCode.E)) isCombatMode = !isCombatMode;
    }

    private void FlipGizmos()
    {
        if (sprite.flipX) attackPoint.transform.localPosition = new Vector3(-0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
            else
                attackPoint.transform.localPosition = new Vector3(0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
    }

    private void Attack() {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1") && isCombatMode)
            {
                animator.SetBool("isAttack", true);

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

                foreach(Collider2D enemy in hitEnemies)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                    }

                nextAttackTime = Time.time + 1F / attackRate;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
       // if (attackPointLeft == null)
       //    return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
       // Gizmos.DrawWireSphere(attackPointLeft.position, attackRangeLeft);
    }
}