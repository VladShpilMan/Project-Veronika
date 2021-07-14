using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool _isGround;

    [HideInInspector] public new Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private InputComponent _input;
    private PhysicsComponent _physics;
    private GraphicsComponent _graphics;
    private AttackComponent _attack;

    public bool IsGround => _isGround;
    public Animator GetAnimator => _animator;
    public SpriteRenderer Sprite => _sprite;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;

    private void Start()
    {
        _input = GetComponent<InputComponent>();
        _physics = GetComponent<PhysicsComponent>();
        _graphics = GetComponent<GraphicsComponent>();
        _attack = GetComponent<AttackComponent>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        _input.InputUpdate(this);
        _physics.PhysicsComponentUpdate(this, _input);
        _graphics.GraphicsUpdate(this, _input);
        _attack.AttackUpdate(this, _input);
    }

    private void FixedUpdate()
    {
        _physics.PhysicsComponentFixedUpdate(this, _input);
    }

}
