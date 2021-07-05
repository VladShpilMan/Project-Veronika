using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //protected Animator animator;
    //[SerializeField] private float attackDistance;
    //private float nextShootTime = 0F;
    //private float shootRate = 1f;
    //private SpriteRenderer sprite;

    //private Arow arow;

    //private void Awake()
    //{
    //    arow = Resources.Load<Arow>("Arow");
    //    sprite = GetComponentInChildren<SpriteRenderer>();
    //    animator = GetComponent<Animator>();
    //}

    // private void FixedUpdate()
    //{
    //    RaycastHit2D[] whatSees = Physics2D.RaycastAll(transform.position, transform.localScale.x * Vector2.right, attackDistance);

    //    foreach (RaycastHit2D rey in whatSees)
    //    {
    //        if (rey.collider.tag.Equals("Character"))
    //        {
    //            Attack();
    //        }
    //    }
    //}

    //private void Attack()
    //{
    //    movingRight = transform.position.x > character.position.x;
    //    sprite.flipX = movingRight;
    //    animator.SetBool("isMove", false);
    //    if (Time.time >= nextShootTime)
    //    {
    //        animator.SetTrigger("isShoot");

    //        StartCoroutine(WaitAndAttack(0.5F));
    //        nextShootTime = Time.time + 1F / shootRate;
    //    }
    //}

    //IEnumerator WaitAndAttack(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    Fire();
    //}

    //private void Fire()
    //{
    //    Vector3 position = transform.position; position.y += 0.1122F; position.x -= 0.1F;
    //    Arow newArow = Instantiate(arow, position, arow.transform.rotation) as Arow;

    //    newArow.Parent = gameObject;

    //    if (!sprite.flipX) newArow.Direction = newArow.transform.right;
    //    else newArow.Direction = -newArow.transform.right;
    //}
}
