using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock2 : BlockController
{
    private Vector2 _upTargetPos;
    private Vector2 _downTargetPos;
    private bool _isTransition;
    public float _speed = 5f;
    private float _yIndex;
    protected override void Init()
    {
        base.Init();
        _upTargetPos = transform.position + new Vector3(0, 10f, 0);
        _downTargetPos = transform.position + new Vector3(0, -10f, 0);
        _isTransition = false;
        _yIndex = 1f;
        StartCoroutine(MoveTarget());
    }

    IEnumerator MoveTarget()
    {
        while (true)
        {
            if (_isTransition)
                _isTransition = false;
            yield return YieldInstructionCache.WaitForSeconds(1);
        }
    }

    private void Move()
    {
        Vector2 curPos = transform.position;
        Vector2 moveDir = Vector2.up * _yIndex * _speed * Time.deltaTime;
        transform.position = curPos + moveDir;

    }

    private void Update()
    {
        Move();
        if (Vector2.Distance(transform.position, _upTargetPos) < 0.1f)
        {
            _isTransition = true;
            _yIndex *= -1;
        }
        else if (Vector2.Distance(transform.position, _downTargetPos) < 0.1f)
        {
            _isTransition = true;
            _yIndex *= -1;
        }
    }
}
