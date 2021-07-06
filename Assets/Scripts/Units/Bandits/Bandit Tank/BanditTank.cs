using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditTank : Enemy
{

    private bool inDefence;
    [SerializeField] private Transform point;

    [SerializeField] private float defenceDistance;
    [SerializeField] private float watchingDistance;

    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        CheckGround();
        EnemyLogic();

        if (chill) Chill();
        if (inDefence) Defence();
        Debug.Log(Vector2.Distance(transform.position, character.position) < defenceDistance);
    }

    private void EnemyLogic()
    {
        if (Vector2.Distance(transform.position, character.position) < watchingDistance)
        {
            inDefence = true;
            chill = false;
        }
        else inDefence = false; 

        if(!inDefence)
        {
            chill = true;
        }
    }

    private void Chill()
    {
        animator.SetBool("isMove", false);
    }

    private void Defence()
    {    
        movingRight = transform.position.x < character.position.x;
        sprite.flipX = !movingRight;

        if (Vector2.Distance(transform.position, point.position) < defenceDistance && Vector2.Distance(transform.position, character.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, character.position, speed);
            animator.SetBool("isMove", true);
        }
        else animator.SetBool("isMove", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(point.position, point.position + defenceDistance * Vector3.right);
    }

    protected override void Die()
    {
        Debug.Log("Die " + this.gameObject);
        animator.SetBool("isDie", true);
        animator.SetTrigger("Hit");
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        GetComponent<TankAttack>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
