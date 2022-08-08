using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator _animator;
    SpriteRenderer _sprite;
    public void Init()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Hited()
    {
        CameraController.ShouldShake = true;
        _sprite.color = Color.red;
    }

    public void Return()
    {
        _sprite.color = Color.white;
    }

    public void PlayerRun(bool state,bool flipX)
    {
        _sprite.flipX = flipX;
        _animator.SetBool("IsMove", state);
    }

    public void IsGrounded(bool state)
    {
        _animator.SetBool("IsGrounded", state);
    }

    public void PlayerJump()
    {
        _animator.SetTrigger("DoJump");
    }

}
