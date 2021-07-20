﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region DELEGATES and EVENTS
    public delegate void HealthChange(float argument, float health);
    public event HealthChange healthChange;

    public delegate void HitSound(string argument);
    public event HitSound hitSound;
    #endregion

    #region ISPECTOR DATAS
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    #endregion

    #region PUBLIC
    public bool IsGround => _isGround;
    public Animator GetAnimator => _animator;
    public SpriteRenderer Sprite => _sprite;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    public Transform GroundCheck => _groundCheck;
    public float CheckRadius => _checkRadius;
    public LayerMask WhatIsGround => _whatIsGround;

    [HideInInspector] public Rigidbody2D _rigidbody;
    #endregion

    #region PRIVATE
    private Animator _animator;
    private SpriteRenderer _sprite;
    private InputComponent _input;
    private PhysicsComponent _physics;
    private GraphicsComponent _graphics;
    private AttackComponent _attack;
    private SoundComponent _sound;
    private StepsSound _steps;
    private bool _isGround;
    #endregion


    #region MONO
    private void Start()
    {
        GetReferences();
        _sound.SoundStart(this, _input);
        _steps.StepsStart(_input);
    }

    private void GetReferences()
    {
        _input = GetComponent<InputComponent>();
        _physics = GetComponent<PhysicsComponent>();
        _graphics = GetComponent<GraphicsComponent>();
        _attack = GetComponent<AttackComponent>();
        _sound = GetComponent<SoundComponent>();
        _steps = GetComponentInChildren<StepsSound>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }
    #endregion

    private void Update()
    {
        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
        _input.InputUpdate(this);
        _physics.PhysicsComponentUpdate(this, _input);
        _graphics.GraphicsUpdate(this, _input);
        _attack.AttackUpdate(this, _input);
    }

    private void FixedUpdate()
    {
        _physics.PhysicsComponentFixedUpdate(this, _input);
    }

    public void TakeDamage(int damage, float repulsion)
    {
        _health -= damage;
        if (_health > 0)
        {
            _animator.SetTrigger("Hit");
            _animator.SetBool("isDie", false);
            _rigidbody.AddForce(transform.up * 1.5F, ForceMode2D.Impulse);
            if (repulsion != 0) _rigidbody.AddForce(transform.right * repulsion, ForceMode2D.Impulse);

            healthChange(damage, _health);
            hitSound("Hit");
        }
        else Die();
    }

    protected void Die()
    {
        Debug.Log("Die " + this.gameObject);
        _animator.SetBool("isDie", true);
        _animator.SetTrigger("Hit");
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}