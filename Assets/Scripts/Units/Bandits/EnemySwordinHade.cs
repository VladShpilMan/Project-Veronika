using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordinHade : Enemy {

    [SerializeField] private Transform point;
    [SerializeField] private int positionOfPatrol;
    [SerializeField] protected float stoppingDistance;
    [SerializeField] private float expectation;

    void FixedUpdate() {
        EnemyLogic();

        if (chill) Chill();
        if (angry) Angry();
        if (goBack) GoBack();
    }

    private void EnemyLogic() {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
            angry = false;
            goBack = false;
        }

        if (Vector2.Distance(transform.position, character.position) < stoppingDistance)
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
        if(character.transform.position.x > transform.position.x) sprite.flipX = false;
            else sprite.flipX = true;
    }

    void GoBack() {
        animator.SetBool("inMove", true);
        if (point.position.x - transform.position.x > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
        speedAtMoment = ((float)speed / 100) + 0.02f;
        transform.position = Vector2.MoveTowards(transform.position, point.position, speedAtMoment);
    }


    public override void TakeDamage(int damage) {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        animator.SetBool("isDie", false);

        if (currentHealth <= 0)
            Die();
    }
}
