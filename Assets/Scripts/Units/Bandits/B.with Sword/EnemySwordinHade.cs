using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordinHade : Enemy {

    [SerializeField] private Transform point;
    [SerializeField] private int positionOfPatrol;
    
    [SerializeField] private float expectation;
    [SerializeField] private float rayDistance = 3F;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    private bool isGround;

    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
    }


    void FixedUpdate() {
        EnemyLogic();
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        animator.SetBool("isGround", isGround);

        
        if (chill) Chill(); 
        if (angry) Angry();
        if (goBack) GoBack();

        Jump();

    }

    private void EnemyLogic() {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
            angry = false;
            goBack = false;
        }

        if (Vector2.Distance(transform.position, character.position) < stoppingDistance && !Character.IsDie)
        {         
             angry = true;
             chill = false;
             goBack = false;           
        }

        if (Vector2.Distance(transform.position, character.position) > stoppingDistance && chill == false)
        {
            angry = false;
            goBack = true;
            chill = false;
        }
    
    }

    void Chill() {
        animator.SetBool("inMove", true);
        speedAtMoment = (float)speed / 100;
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
            transform.position = new Vector2(transform.position.x + speedAtMoment, transform.position.y);
        }
        else
        {
            sprite.flipX = true;
            transform.position = new Vector2(transform.position.x - speedAtMoment, transform.position.y);
        }
    }


    void Angry()
    {
        
        double chek = transform.position.x - character.position.x;
        if (chek < 0) chek *= -1;
        FlipX();
        // the gameObject approaches the MainCharacter if the distance between them is greater than the parameter
        if (chek >= 1.2)
        {
            speedAtMoment = ((float)speed / 100) + 0.02f;
            transform.position = Vector2.MoveTowards(transform.position, character.position, speedAtMoment);
            animator.SetBool("inMove", true);
        }     
        
        if(chek < 1.2)
            animator.SetBool("inMove", false);
    }

    private void FlipX() {
        if (character.transform.position.x > transform.position.x)
        {
            sprite.flipX = false;
            movingRight = true;
        }
        else
        {
            sprite.flipX = true;
            movingRight = false;
        }
    }

    void GoBack() {
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

    private void Jump()
    {
        if (movingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);

            if (hit.collider != null && !hit.collider.tag.Equals("Character"))
            {
                rigidbody.velocity = Vector2.up * jumpForce;
            }

        }
        else if (!movingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, rayDistance);
            if (hit.collider != null && !hit.collider.tag.Equals("Character"))
            {
                rigidbody.velocity = Vector2.up * jumpForce;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * rayDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayDistance);

    }
}
