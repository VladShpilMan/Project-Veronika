using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foe : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;

    [SerializeField] private float stoppingDistance;
    [SerializeField] private int positionOfPatrol;
    [SerializeField] private Transform point;
    private Transform character;

    private GuardianAIComponent _logic;

    public Animator GetAnimator => _animator;
    public SpriteRenderer Sprite => _sprite;

    private void Start()
    {
        _logic = GetComponent<GuardianAIComponent>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _logic.GuardianAIUpdate();
    }

}
