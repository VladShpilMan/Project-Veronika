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
    [SerializeField] private Transform point;
    private Arow arow;
     
    protected void Awake()
    {
        arow = Resources.Load<Arow>("Arow");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        EnemyLogic();

        if (comeUp) ComeUp();
        if (angry) Attack();
        if (goBack) GoBack();
        if (!inSafety) toKeepDistance();
    }

    private void EnemyLogic() {

        //RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.localScale.x * Vector2.right, watchingDistance);
        //foreach (RaycastHit2D rey in hit)
        //    if (rey.collider.tag.Equals("Character") && inSafety) canShoot = true;
        //        else canShoot = false;
        //if (canShoot)
        //{ 
        float cordMax = transform.position.y + 1f;
        float cordMin = transform.position.y - 1f;
        Debug.Log(cordMax > character.transform.position.y && cordMin < character.transform.position.y);
        if (cordMax > character.transform.position.y && cordMin < character.transform.position.y && inSafety)
        {
            if (Vector2.Distance(transform.position, character.position) < attackDistance && !Character.IsDie)
            {
                angry = true;
                comeUp = false;
                goBack = false;
                chill = false;
            }

            if (Vector2.Distance(transform.position, character.position) < watchingDistance &&
                Vector2.Distance(transform.position, character.position) > attackDistance &&
                !Character.IsDie)
            {
                comeUp = true;
                angry = false;
                goBack = false;
                chill = false;
            }
        }

        if(Vector2.Distance(transform.position, character.position) > watchingDistance)
        {
            angry = false;
            comeUp = false;
            goBack = true;
        }

        if(Vector2.Distance(transform.position, character.position) < stoppingDistance)
        {
            angry = false;
            inSafety = false;
        }
    }

    private void ComeUp()
    {
        transform.position = Vector2.MoveTowards(transform.position, character.position, speed);
        animator.SetBool("isMove", true);
    }


    private void Attack() {
        movingRight = transform.position.x > character.position.x;
        sprite.flipX = movingRight;
        animator.SetBool("isMove", false);
        if (Time.time >= nextShootTime)
            {
                animator.SetTrigger("isShoot");
                
                StartCoroutine(WaitAndAttack(0.5F));
                nextShootTime = Time.time + 1F / shootRate;
            }
    }

    IEnumerator WaitAndAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Shoot();
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.1122F; position.x -= 0.1F;
        Arow newArow = Instantiate(arow, position, arow.transform.rotation) as Arow;

        newArow.Parent = gameObject;

        if (!sprite.flipX) newArow.Direction = newArow.transform.right;
        else newArow.Direction = -newArow.transform.right;
    }

    private void toKeepDistance()
    {

        int direction = transform.position.x > character.position.x ? 1 : -1;
        float chek = Mathf.Abs(transform.position.x - character.position.x);
        sprite.flipX = direction < 0.0F;

        if (chek < 5)
        {
            animator.SetBool("isMove", true);
            transform.Translate(speed * direction, 0, 0);
        }
        else
            inSafety = true;
    }

    private void GoBack()
    {
        animator.SetBool("inMove", true);
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
        Gizmos.DrawLine(transform.position, transform.position + watchingDistance * Vector3.right);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + attackDistance * Vector3.right);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + stoppingDistance * Vector3.right);
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
