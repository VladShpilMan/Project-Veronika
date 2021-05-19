using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordinHade : Enemy {

    [SerializeField] private Transform point;
    [SerializeField] private int positionOfPatrol;
    [SerializeField] protected float stoppingDistance;

    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false) chill = true;

        if (Vector2.Distance(transform.position, character.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        //if (Vector2.Distance(transform.position, character.position) > stoppingDistance)
        //{
        //    angry = false;
        //    goBack = true;
        //}

        if (chill) Chill();
        if (angry) Angry();
        //if (goBack) GoBack();
    }

    void Chill()
    {
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
            transform.Translate(speedAtMoment, 0, 0);
        }
        else
        {
            sprite.flipX = true;
            transform.Translate(-speedAtMoment, 0, 0);
        }
    }

    void Angry()
    {
        double chek = transform.position.x - character.position.x;
        if (chek < 0) chek *= -1;

        // the gameObject approaches the MainCharacter if the distance between them is greater than the parameter
        if (chek >= 1.2)
        {
            if (transform.position.x - character.position.x > 0 && movingRight == true)
            {  // flip the sprite horizontally
                sprite.flipX = true;
            }

            if (transform.position.x - character.position.x < 0 && movingRight == false)
            { //flip the sprite horizontally
                sprite.flipX = false;
            }


// the gameObject increases its speed in case of aggression

            if (movingRight)
            {
                transform.Translate(speedAtMoment, 0, 0);
            }
            else
            {
                transform.Translate(-speedAtMoment, 0, 0);
            }
        }
    }

    void GoBack()
    {
        if(point.position.x - transform.position.x > 0)
        {
            sprite.flipX = false;
            transform.Translate(speedAtMoment, 0, 0);
        }
        else
        {
            sprite.flipX = true;
            transform.Translate(-speedAtMoment, 0, 0);
        }
    }
}
