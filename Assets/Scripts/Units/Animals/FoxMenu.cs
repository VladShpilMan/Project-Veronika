using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMenu : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            animator.SetBool("getUp", true);
            animator.SetBool("idel", true);
            yield return new WaitForSeconds(2f);
            animator.SetBool("getUp", false);
            animator.SetBool("idel", false);
        }
    }
}
