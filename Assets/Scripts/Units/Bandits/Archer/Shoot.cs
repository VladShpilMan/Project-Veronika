using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] private float attackDistance;
    private float nextShootTime = 0F;
    private float shootRate = 1f;
    private SpriteRenderer sprite;

    private Arow arow;

    private void Awake()
    {
        arow = Resources.Load<Arow>("Arow");
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D[] whatSees;
        if (BanditArcerh.MovingRight) whatSees = Physics2D.RaycastAll(transform.position, transform.localScale.x * Vector2.right, attackDistance);
            else whatSees = Physics2D.RaycastAll(transform.position, transform.localScale.x * Vector2.left, attackDistance);
        foreach (RaycastHit2D rey in whatSees)
        {
            if (rey.collider.tag.Equals("Character") && BanditArcerh._Angry)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        sprite.flipX = BanditArcerh.MovingRight;
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
        Fire();
    }

    private void Fire()
    {
        Vector3 position = transform.position; position.y += 0.1122F; position.x -= 0.1F;
        Arow newArow = Instantiate(arow, position, arow.transform.rotation) as Arow;

        newArow.Parent = gameObject;

        if (!sprite.flipX) newArow.Direction = newArow.transform.right;
        else newArow.Direction = -newArow.transform.right;
    }
}
