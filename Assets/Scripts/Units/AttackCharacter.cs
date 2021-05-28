using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharacter : MonoBehaviour
{
    private Animator animator;
    private float nextAttackTime = 0F;
    private float attackRate = 2.5f;

    [SerializeField]private Transform attackPointRight;
    [SerializeField]private Transform attackPointLeft;
    [SerializeField]private float attackRangeRight;
    [SerializeField]private float attackRangeLeft;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("Attack", false);
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1F / attackRate;
            }
        }
    }

    void Attack()
    {
        animator.SetBool("Attack", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRangeRight);

    }

    void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
            return;
        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireSphere(attackPointRight.position, attackRangeRight);
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRangeLeft);
    }
}
