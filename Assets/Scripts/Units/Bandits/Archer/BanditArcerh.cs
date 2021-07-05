using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcerh : Enemy
{

    private float nextShootTime = 0F;
    private float shootRate = 1f;
    private bool inSafety = true;
    private bool comeUp;
    private bool canShoot;
    [SerializeField] private float attackDistance;
    [SerializeField] private float watchingDistance;
    [SerializeField] private int dangerousDistance;
    [SerializeField] private Transform point;
    [SerializeField] private int positionOfPatrol;
    
    private Arow arow;
     
    protected void Awake()
    {
        arow = Resources.Load<Arow>("Arow");
        sprite = GetComponentInChildren<SpriteRenderer>();
        movingRight = true;
        inSafety = true;
    }
    
    private void FixedUpdate()
    {
        EnemyLogic();

        if (angry) Angry();
        if (goBack) GoBack();
        if (chill) Chill();
        if (!inSafety) toKeepDistance();
    }

    private void EnemyLogic() {

        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && !angry && inSafety)
        {
            chill = true;
            angry = false;
            goBack = false;
            inSafety = true;
        }

        if (Vector2.Distance(transform.position, character.position) < watchingDistance && inSafety)
        {
            angry = true;
            goBack = false;
            chill = false;
            inSafety = true;
        }

        if (Vector2.Distance(transform.position, character.position) > watchingDistance && !chill)
        {
            goBack = true;
            angry = false;
            chill = false;
            inSafety = true;
        }

        if (Vector2.Distance(transform.position, character.position) < dangerousDistance)
        {
            goBack = false;
            angry = false;
            chill = false;
            inSafety = false;
        }
    }

    private void Angry() {
        movingRight = transform.position.x < character.position.x;
        sprite.flipX = !movingRight;

        if (Vector2.Distance(transform.position, character.position) > attackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, character.position, speed);
            animator.SetBool("isMove", true);
        }
        else animator.SetBool("isMove", false);

    }

    private void toKeepDistance()
    {

        int direction = transform.position.x > character.position.x ? 1 : -1;
        float chek = Mathf.Abs(transform.position.x - character.position.x);
        sprite.flipX = direction < 0.0F;

        if (chek < stoppingDistance)
        {
            animator.SetBool("isMove", true);
            transform.Translate(speed * direction, 0, 0);
        }
        else
            inSafety = true;

    }

    private void Chill()
    {
        animator.SetBool("isMove", true);
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            sprite.flipX = false;
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }
        else
        {
            sprite.flipX = true;
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        }
    }

    private void GoBack()
    {
        animator.SetBool("isMove", true);
        if (point.position.x - transform.position.x > 0)
        {
            sprite.flipX = false;
            movingRight = true;
        }
        else
        {
            sprite.flipX = true;
            movingRight = false;
        }
        speedAtMoment = ((float)speed / 100) + 0.02f;
        transform.position = Vector2.MoveTowards(transform.position, point.position, speedAtMoment);
    }


    private void OnDrawGizmos()
    {
        if (movingRight)
        {
            Gizmos.DrawLine(transform.position, transform.position + watchingDistance * Vector3.right);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + attackDistance * Vector3.right);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + dangerousDistance * Vector3.right);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + watchingDistance * Vector3.left);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + attackDistance * Vector3.left);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + dangerousDistance * Vector3.left);
        }
    }

    protected override void Die()
    {
        Debug.Log("Die " + this.gameObject);
        //animator.SetBool("isDie", true);
        //animator.SetTrigger("Hit");
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        //GetComponent<EnemyAttack>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
