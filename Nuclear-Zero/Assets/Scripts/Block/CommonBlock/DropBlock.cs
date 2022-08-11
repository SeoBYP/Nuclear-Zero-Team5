using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlock : BlockController
{
    [SerializeField] private Transform _downTargetPos;
    public float _speed = 30f;
    public bool _IsMove;
    protected override void Init()
    {
        base.Init();
    }

    public override void OnSteped()
    {
        base.OnSteped();
        _collider2D.enabled = false;
        _player.TakeDamage();
    }

    public override void OnExited()
    {
        base.OnExited();
        _collider2D.enabled = true;
    }

    private void FixedUpdate()
    {
        if (_IsMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, _downTargetPos.position, Time.deltaTime * _speed);
            if(Vector2.Distance(transform.position, _downTargetPos.position) < 0.05f)
            {
                _trriger2D.enabled = false;
                _IsMove = false;
            }
        }
        
    }

}
