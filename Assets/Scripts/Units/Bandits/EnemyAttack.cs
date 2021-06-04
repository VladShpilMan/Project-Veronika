using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    protected Transform character;
    private Animator animator;
    private float nextAttackTime = 0F;
    private float attackRate = 0.75f;
    [SerializeField] private int attackDamage;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    private SpriteRenderer sprite;

    private void Start() {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        character = GameObject.FindGameObjectWithTag("Character").transform;
    }

    private void Update() {
        FlipGizmos();
        Attack();
    }

    private void Attack() {
        double chek = transform.position.x - character.position.x;
            if (chek < 0) chek *= -1;

        if (chek < 1.2)
        {
            if (Time.time >= nextAttackTime)
            {
                //animator.SetBool("isAttack", true);
                    animator.SetTrigger("Attack");
                    Collider2D[] hitCharacter = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

                    foreach (Collider2D character in hitCharacter)
                    {
                        character.GetComponent<Character>().TakeDamage(attackDamage);
                    }

                    nextAttackTime = Time.time + 1F / attackRate;               
            }
        }
    }

    private void FlipGizmos() {
        if (sprite.flipX) attackPoint.transform.localPosition = new Vector3(-0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
        else
            attackPoint.transform.localPosition = new Vector3(0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
