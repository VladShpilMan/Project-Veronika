using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcerh : Enemy
{

    private float nextShootTime = 0F;
    private float shootRate = 1f;

    private Arow arow;
     
    protected void Awake()
    {
        arow = Resources.Load<Arow>("Arow");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        EnemyLogic();

        if (angry) Angry();
    }

    private void EnemyLogic() {
       
        if (transform.position.y > character.transform.position.y - 2 && transform.position.y < character.transform.position.y + 2)
        {
            if (Vector2.Distance(transform.position, character.position) < stoppingDistance && !Character.IsDie)
            {
                angry = true;
            }
            else
                angry = false;
        }
        else 
            angry = false;
    }

    private void Angry() {
            if (Time.time >= nextShootTime)
            {
                animator.SetTrigger("isShoot");
                Shoot();
                nextShootTime = Time.time + 1F / shootRate;
            }
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.1122F; position.x -= 0.1F;
        Arow newArow = Instantiate(arow, position, arow.transform.rotation) as Arow;

        newArow.Parent = gameObject;

        if (!sprite.flipX) newArow.Direction = newArow.transform.right;
        else newArow.Direction = -newArow.transform.right;
    }

    private void OnDrawGizmos()
    {
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
