using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsComponent : MonoBehaviour
{

    public void GraphicsUpdate(Player player, InputComponent _input)
    {
        player.GetAnimator.SetBool("isGround", player.IsGround);
        player.GetAnimator.SetBool("inMove", _input.IsMove);
        player.GetAnimator.SetBool("isBattleMode", _input.IsBattleMode);

        Run(player, _input);
        Attack(player, _input);
    }

    private void Run(Player player, InputComponent _input)
    {
        if (_input.IsMove)
        {
            if (_input.MovingRight)  player.Sprite.flipX = false;
                else player.Sprite.flipX = true;
        }
    }

    private void Attack(Player player, InputComponent _input)
    {
        if (_input.IsAttack) player.GetAnimator.SetTrigger("Attack");
    }
}
