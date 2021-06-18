using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Squirrel : Unit {
    [SerializeField] private Transform[] points; // An array of points along which the unit can move
    private bool onWay;
    private bool isIdle = false;
    private bool nextPoint = true;
    private int randPoint;
    private int temp;

    private float nextGoTime = 0F;

    private void Start() {
        currentHealth = maxHealth;
        GetReferences();
    }

    private void GetReferences() { // Function for getting components
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() {

        RouteMovement();       
    }

    private void RouteMovement() {
        animator.SetBool("isMove", true);
        FlipX();
        //If the unit can choose the next point and the unit does not stand still
        if (nextPoint && !isIdle) {
            NewRandomPoint();
        }

        if (onWay && !isIdle)
            transform.position = Vector2.MoveTowards(transform.position, points[randPoint].position, 0.03f);


        //If the unit is on the target point and the unit is not standing
        if ((transform.position.x == points[randPoint].position.x) && !isIdle) {
            nextPoint = true;
            onWay = false;
            isIdle = true;
            //The "nextGoTime" variable takes on the value at what moment in time the unit arrived at the specified point + how long it will stay at this point.
            nextGoTime = Time.time + Random.Range(1, 4); 
        }

        if (isIdle) Idel();
    }

    private void NewRandomPoint() {
        //An endless cycle until the number of the new point is randomly selected and 
        //the number of the new point should not be equal to the number of the point at this moment.
        while (true)
        {
            temp = Random.Range(0, points.Length);
            if (randPoint != temp)
            {
                randPoint = temp;
                break;
            }
        }
        nextPoint = false;
        onWay = true;
    }

    //The function causes the unit to be turned towards the point to which it is running.
    private void FlipX() {
        if(transform.position.x > points[randPoint].position.x)
            sprite.flipX = true;
        else 
            sprite.flipX = false;
    }

    //The unit stands still as long as the time from its appearance is less than the time when the unit stepped on the selected point + how long it will stay at this point.
    private void Idel() {
        if (Time.time <= nextGoTime)
        {
            animator.SetBool("isMove", false);
            animator.SetInteger("number", Random.Range(1, 3)); // Idle animation selection
        }
        else isIdle = false;
    }

    protected override void Die() {
        Debug.Log("Die " + this.gameObject);
        animator.SetBool("isDie", true);
        animator.SetTrigger("hit");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
