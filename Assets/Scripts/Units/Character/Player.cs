using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    private bool _isGround;

    public new Rigidbody2D _rigidbody;

    private InputComponent _input;
    private PhysicsComponent _physics;

    public bool IsGround => _isGround;

    private void Start()
    {
        _input = GetComponent<InputComponent>();
        _physics = GetComponent<PhysicsComponent>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //_isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        _input.InputUpdate(this);
        _physics.PhysicsComponentUpdate(this, _input);
    }

}
