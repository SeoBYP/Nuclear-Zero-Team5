using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock1 : BlockController
{
    private Vector2 _leftTargetPos;
    private Vector2 _rightTargetPos;
    private bool _isTransition;
    public float _speed = 5f;
    private float _xIndex;
    public float X_Move;
    protected override void Init()
    {
        base.Init();
        _leftTargetPos = transform.position + new Vector3(X_Move, 0, 0);
        _rightTargetPos = transform.position + new Vector3(-X_Move, 0, 0);
        print($"_leftTargetPos : {_leftTargetPos}, _rightTargetPos:{_rightTargetPos}");
        _isTransition = false;
        _xIndex = 1f;
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
        Vector2 moveDir = Vector2.left * _xIndex * _speed * Time.deltaTime;
        transform.position = curPos + moveDir;

    }

    private void Update()
    {
        Move();
        if (Vector2.Distance(transform.position, _leftTargetPos) <= 0.5f)
        {
            _isTransition = true;
            _xIndex *= -1;
        }
        else if (Vector2.Distance(transform.position, _rightTargetPos) <= 0.5f)
        {
            _isTransition = true;
            _xIndex *= -1;
        }
    }
}
