using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    #region DELEGATES and EVENTS
    public delegate void SomeSound(string argument);
    public delegate void StepSound(string kind, InputComponent input);

    public event SomeSound someSound;
    public event StepSound stepSound;
    #endregion

    #region PRIVATE
    private Vector3 _direction;
    private bool _isMove, _movingRight, _isBattleMode, _isJump, _isAttact;

    private float nextAttackTime = 0F;
    private float attackRate = 2f;
    #endregion

    #region PUBLIC
    public Vector3 Diraction => _direction;
    public bool IsMove => _isMove;
    public bool MovingRight => _movingRight;
    public bool IsBattleMode => _isBattleMode;
    public bool IsJump => _isJump;
    public bool IsAttack => _isAttact;
    #endregion

    public void InputUpdate(Player player)
    {
        Run(player);
        Jump(player);
        BattleMode();
        Attack();
    }

    #region PRIVATE METODS
    private void Run(Player player)
    {
        if (Input.GetButton("Horizontal"))
        {
            _direction = player.transform.right * Input.GetAxis("Horizontal");
            _isMove = true;
            _movingRight = _direction.x > 0.0F;

            stepSound(GetTag(player), this);
        }
        else
        {
            _isMove = false;
            stepSound(null, this);
        }
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
                    someSound("Attack");
                }   
                    else _isAttact = false;
            }
                else _isAttact = false;
    }

    private string GetTag(Player player) { 
        // Looks for the tag of the surface the player is standing on
        //All colliders of objects on the ground layer are written to an array

        string tagName = null;

        Collider2D[] groundCollider = Physics2D.OverlapCircleAll(player.GroundCheck.position, player.CheckRadius, player.WhatIsGround);

        foreach (Collider2D col in groundCollider)
            tagName = col.gameObject.tag;

        return tagName;
    }
    #endregion
}
