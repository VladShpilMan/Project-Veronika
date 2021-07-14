using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour {

    public void PhysicsComponentUpdate(Player player, InputComponent _input)
    {
        Jump(player, _input);
    }

    public void PhysicsComponentFixedUpdate(Player player, InputComponent _input)
    {
        Run(player, _input);
    }

    private void Run(Player player, InputComponent _input)
    {
        if (_input.IsMove)
        {
            player.transform.Translate(player.Speed * _input.Diraction.x, 0, 0);
        }
    }

    public void Jump(Player player, InputComponent _input)
    {
        if (_input.IsJump)
        {
            player._rigidbody.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
        }
    }
}
