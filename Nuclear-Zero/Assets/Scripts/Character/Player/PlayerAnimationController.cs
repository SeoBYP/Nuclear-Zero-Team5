using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator _animator;
    SpriteRenderer _sprite;
    public void Init()
    {
        //_animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Hited()
    {
        _sprite.color = Color.red;
    }

    public void Return()
    {
        _sprite.color = Color.white;
    }

    public void PlayRightWalk()
    {
        _animator.Play("PlayerRightWalk");
    }
    public void PlayUpWalk()
    {
        _animator.Play("PlayerUpWalk");
    }

    public void PlayDownWalk()
    {
        _animator.Play("PlayerDownWalk");
    }
}
