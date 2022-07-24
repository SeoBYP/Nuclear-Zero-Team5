using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : EnemyController
{
    public Vector3 _Offset;
    private Vector2 dir;

    private Vector2 _targetPos;
    private Vector2 _startPos;

    private float elapsed = 0;

    private float _speed = 1;
    public float DeActiveTime = 3;
    public float _shotTime;

    private bool IsMoveDown;
    private bool IsMoveUp;
    private bool IsFollow;
    private bool isUpdate = false;

    public override void Init()
    {
        base.Init();
        _player = Utils.FindObjectOfType<PlayerController>();
        _targetPos = _player.transform.position + _Offset;
        StartCoroutine(SetTarget());
        StartCoroutine(DeActiveTimer());
        IsMoveDown = true;
    }

    protected override void Run()
    {
        base.Run();
        Move();
        FollowPlayer();
        Shot();
    }

    IEnumerator SetTarget()
    {
        while (true)
        {
            yield return YieldInstructionCache.WaitForSeconds(_shotTime);
            dir = _player.transform.position - transform.position;
            isUpdate = true;
            Shot();
        }
    }

    IEnumerator DeActiveTimer()
    {
        yield return YieldInstructionCache.WaitForSeconds(DeActiveTime);
        IsMoveUp = true;
        IsFollow = false;
    }

    private void Shot()
    {
        if (IsFollow == false)
            return;
        if (isUpdate == false)
            return;

        elapsed += Time.deltaTime / _speed;
        elapsed = Mathf.Clamp01(elapsed);
        if(elapsed >= 0)
        {
            GameObject go = ResourcesManager.Instance.Instantiate("Weapon/Arrow");
            go.transform.position = this.transform.position;
            Arrow arrow = go.GetComponent<Arrow>();
            arrow.Init();
            arrow.SetTargetDir(dir);
            isUpdate = false;
            elapsed = 0;
        }
        
    }

    private void Move()
    {
        if (IsMoveDown)
        {
            transform.position = Vector2.Lerp(transform.position, _targetPos, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _targetPos) < 0.1f)
            {
                IsMoveDown = false;
                IsFollow = true;
                
            }                
        }
        if (IsMoveUp)
        {
            transform.position = Vector2.Lerp(transform.position, _startPos, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _startPos) < 0.1f)
            {
                IsMoveUp = false;
                IsFollow = false;
            }
        }
    }

    private void FollowPlayer()
    {
        if (IsFollow)
        {
            Vector3 playerPos = _player.transform.position + _Offset;
            transform.position = playerPos;
            _startPos = transform.position + _Offset;
        }
    }
}
