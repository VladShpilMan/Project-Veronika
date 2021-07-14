using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private float repulsion;

    [SerializeField] private Transform attackPoint; // This point is the center of the radius of the circle of the attack zone
    [SerializeField] private float attackRange; //Attack radius
    [SerializeField] private LayerMask enemyLayer;

    public void AttackUpdate(Player player, InputComponent _input)
    {
        Attack(_input);
        FlipGizmos(player);
    }

    private void Attack(InputComponent _input)
    {
        if (_input.IsAttack)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Unit>().TakeDamage(attackDamage, repulsion);
            }
        }
    }


    private void OnDrawGizmosSelected()
    { // Draws a circle with a radius of attack for visual representation.
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    //Based on the direction in which the player moves, the "attackPoint" moves in respectively direction.
    private void FlipGizmos(Player player)
    {
        if (player.Sprite.flipX) attackPoint.transform.localPosition = new Vector3(-0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
        else
            attackPoint.transform.localPosition = new Vector3(0.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
    }
}
