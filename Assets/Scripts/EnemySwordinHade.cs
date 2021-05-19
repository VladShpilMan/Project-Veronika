using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordinHade : Enemy
{

    [SerializeField] private Transform point;
    [SerializeField] private int positionOfPatrol;

    void FixedUpdate()
    {
        Chill();
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
}
