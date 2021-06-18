using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharacter : MonoBehaviour {
    private bool isCombatMode = false;
    private Animator animator;
    private float nextAttackTime = 0F;
    private float attackRate = 3f;
    [SerializeField] private int attackDamage;

    [SerializeField]private Transform attackPoint; // This point is the center of the radius of the circle of the attack zone
    [SerializeField]private float attackRange; //Attack radius
    [SerializeField]private LayerMask enemyLayer;
    private SpriteRenderer sprite;

    private void Start()
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

    //The method of attack. You can only attack once in a while.
    private void Attack() {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1") && isCombatMode)
            {
                animator.SetBool("isAttack", true);

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

                foreach(Collider2D enemy in hitEnemies)
                    {
                        enemy.GetComponent<Unit>().TakeDamage(attackDamage);
                    }

                nextAttackTime = Time.time + 1F / attackRate;
            }
        }
    }

    private void OnDrawGizmosSelected() { // Draws a circle with a radius of attack for visual representation.
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    //Based on the direction in which the player moves, the "attackPoint" moves in respectively direction.
    private void FlipGizmos() {
        if (sprite.flipX) attackPoint.transform.localPosition = new Vector3(-0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
        else
            attackPoint.transform.localPosition = new Vector3(0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
    }
}
