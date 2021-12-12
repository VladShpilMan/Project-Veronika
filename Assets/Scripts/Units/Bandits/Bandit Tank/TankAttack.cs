using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    protected Transform character;
    private Animator animator;
    private float nextAttackTime = 0F;
    private float attackRate = 0.75f;
    [SerializeField] private int attackDamage;
    [SerializeField] private float repulsion;

    [SerializeField] private Transform attackPoint; // This point is the center of the radius of the circle of the attack zone
    [SerializeField] private float attackRange; // Attack radius
    [SerializeField] private LayerMask enemyLayer;
    private SpriteRenderer sprite;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(StartFunction());
    }

    private IEnumerator StartFunction()
    {
        //Wait for 5 sec.
        yield return new WaitForSeconds(0.1f);

        //Turn My game object that is set to false(off) to True(on).
        character = Player.TrasPos;
    }

    private void Update()
    {
        FlipGizmos();
        if (!Character.IsDie) Attack();
    }

    private void Attack()
    {
        double chek = transform.position.x - character.position.x;
        if (chek < 0) chek *= -1;

        if (chek < 1)
        {
            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("Attack");
                Collider2D[] hitCharacter = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

                foreach (Collider2D character in hitCharacter)
                {

                    StartCoroutine(WaitAndDamage(0.3F));
                }

                nextAttackTime = Time.time + 1F / attackRate;
            }
        }
    }

    IEnumerator WaitAndDamage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        character.GetComponent<Player>().TakeDamage(attackDamage, repulsion);
    }

    private void OnDrawGizmosSelected()
    { // Draws a circle with a radius of attack for visual representation.
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    //Based on the direction in which the player moves, the "attackPoint" moves in respectively direction.
    private void FlipGizmos()
    {
        if (sprite.flipX) attackPoint.transform.localPosition = new Vector3(-0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
        else
            attackPoint.transform.localPosition = new Vector3(0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
    }
}
