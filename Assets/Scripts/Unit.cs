using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected float speed;
    protected SpriteRenderer sprite;
    protected Animator animator;
    protected Rigidbody2D rigidbody;

    public virtual void TakeDamage(float damege)
    {
        Die();
    }

    protected virtual void Die() { }
    
}
