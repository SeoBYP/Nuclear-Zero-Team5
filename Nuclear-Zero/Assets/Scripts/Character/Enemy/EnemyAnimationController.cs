using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator _animator;
    SpriteRenderer _sprite;
    public void Init()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void PlayEnemyWalk()
    {
        _sprite.flipX = true;
        _animator.Play("EnemyWalk");
    }
    public void PlayEnemyShoot()
    {
        _sprite.flipX = true;
        _animator.Play("EnemyAttack");
    }
}
