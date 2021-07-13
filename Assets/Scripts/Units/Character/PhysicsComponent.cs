using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    public void PhysicsComponentUpdate(Player player, InputComponent _input)
    {
        Run(player, _input);
        Jump(player, _input);
    }

    private void Run(Player player, InputComponent _input)
    {
        if (_input.IsMove)
        {
            player.transform.Translate(_speed * _input.Diraction.x, 0, 0);
        }
    }

    public void Jump(Player player, InputComponent _input)
    {
        if (_input.IsJump)
        {
            player._rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

}
