using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    private Vector3 _direction;
    private bool _isMove, _movingRight, _isBattleMode, _isJump, _isAttact;

    private float nextAttackTime = 0F;
    private float attackRate = 2f;

    public Vector3 Diraction => _direction;
    public bool IsMove => _isMove;
    public bool MovingRight => _movingRight;
    public bool IsBattleMode => _isBattleMode;
    public bool IsJump => _isJump;
    public bool IsAttack => _isAttact;

    public void InputUpdate(Player player)
    {
        Run(player);
        Jump(player);
        BattleMode();
        Attack();
    }

    private void Run(Player player)
    {
        if (Input.GetButton("Horizontal"))
        {
            _direction = player.transform.right * Input.GetAxis("Horizontal");
            _isMove = true;
            _movingRight = _direction.x > 0.0F;
        }
        else _isMove = false;
    }

    private void Jump(Player player)
    {
        if (Input.GetButtonDown("Jump") && player.IsGround)
        {
            _isJump = true;
        }
        else _isJump = false;
    }

    private void BattleMode()
    { // Entering combat mode
        if (Input.GetKeyDown(KeyCode.E))
            if (_isBattleMode)
            {
                _isBattleMode = !_isBattleMode;
            }
            else
            {
                _isBattleMode = !_isBattleMode;
            }
    }

    private void Attack()
    {
            if (Input.GetButtonDown("Fire1") && _isBattleMode)
            {
                if (Time.time >= nextAttackTime)
                {
                    _isAttact = true;
                    nextAttackTime = Time.time + 1F / attackRate;
                }   
                    else _isAttact = false;
            }
                else _isAttact = false;
    }
}
