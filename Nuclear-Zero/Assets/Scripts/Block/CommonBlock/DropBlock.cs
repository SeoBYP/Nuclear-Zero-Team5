using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlock : BlockController
{
    private Vector2 _downTargetPos;
    private Vector2 _curPos;
    private bool _isMoveDown;
    public float _speed = 30f;
    private float _yIndex;
    protected override void Init()
    {
        base.Init();
        _curPos = transform.position;
        _downTargetPos = transform.position + new Vector3(0, -30f, 0);
        _isMoveDown = false;
        _yIndex = 0;
        _speed = 0;
    }

    public override void OnSteped()
    {
        base.OnSteped();
        _isMoveDown = true;
        _yIndex = -1;
        _speed = 30f;
    }

    private void Move()
    {
        Vector2 curPos = transform.position;
        Vector2 moveDir = Vector2.up * _yIndex * _speed * Time.deltaTime;
        transform.position = curPos + moveDir;
    }

    private void Update()
    {
        if (_isMoveDown)
        {
            Move();
            if (Vector2.Distance(transform.position, _downTargetPos) < 0.1f)
            {
                _isMoveDown = false;
                _speed = 15f;
                _yIndex = 1;
            }
        }
        else
        {
            Move();
            if (Vector2.Distance(transform.position, _curPos) < 0.1f)
            {
                _speed = 0;
                _yIndex = 0;
            }
        }
    }

}
