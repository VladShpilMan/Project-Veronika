using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    private Vector3 _direction;
    private bool _isMove, _movingRight, _isMode, _isJump; 

    public Vector3 Diraction => _direction;
    public bool IsMove => _isMove;
    public bool MovingRight => _movingRight;
    public bool IsMode => _isMode;
    public bool IsJump => _isJump;

    public void InputUpdate(Player player)
    {
        Run(player);
        Jump();
        BattleMode();
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

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _isJump = true;
        }
        else _isJump = false;
    }

    private void BattleMode()
    { // Entering combat mode
        if (Input.GetKeyDown(KeyCode.E))
            if (_isMode)
            {
                _isMode = !_isMode;
            }
            else
            {
                _isMode = !_isMode;
            }
    }
}
