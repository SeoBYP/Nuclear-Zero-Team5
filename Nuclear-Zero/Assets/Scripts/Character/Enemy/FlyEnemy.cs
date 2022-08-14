using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : EnemyController
{
    public Vector3 _TargetOffset;
    public Vector3 _ReturnOffset;
    private Vector2 dir;
    private Vector2 _targetPos;
    private Vector2 _startPos;

    private Transform endPos;
    private Animation _flyanimation;
    private float elapsed = 0;

    private float _speed = 1;
    public float DeActiveTime = 3;
    public float _shotTime;
    public float AttackRange;

    private bool _IsAttack;
    private bool IsMoveDown;
    private bool IsMoveUp;
    private bool IsFollow;
    private bool isUpdate = false;

    public override void Init()
    {
        base.Init();
        if (_player == null)
            _player = Utils.FindObjectOfType<PlayerController>();
        _flyanimation = GetComponentInChildren<Animation>();
        if (_flyanimation != null)
            _flyanimation.Play();
        _IsAttack = false;
        IsMoveDown = true;
    }

    protected override void Run()
    {
        base.Run();
        CheckPlayer();
        if (_IsAttack)
        {
            Move();
            FollowPlayer();
            isUpdate = true;
            Shot();
        }
    }

    private void CheckPlayer()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < AttackRange)
        {
            _IsAttack = true;
        }
    }

    IEnumerator DeActiveTimer()
    {
        yield return YieldInstructionCache.WaitForSeconds(DeActiveTime);
        IsMoveUp = true;
        IsFollow = false;
        isUpdate = false;
    }

    private void Shot()
    {
        if (IsFollow == false)
            return;
        if (isUpdate == false)
            return;

        elapsed += Time.deltaTime / _speed;
        elapsed = Mathf.Clamp01(elapsed);
        if(elapsed >= _shotTime)
        {
            dir = _player.transform.position - transform.position;
            GameObject go = ResourcesManager.Instance.Instantiate("Weapon/Arrow");
            go.transform.position = this.transform.position;
            Arrow arrow = go.GetComponent<Arrow>();
            arrow.Init();
            arrow.SetTargetDir(dir);
            
            elapsed = 0;
        }
        
    }

    private void Move()
    {
        if (IsMoveDown)
        {
            _targetPos = _player.transform.position + _TargetOffset;
            transform.position = Vector2.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _targetPos) < 0.1f)
            {
                IsMoveDown = false;
                IsFollow = true;
                StartCoroutine(DeActiveTimer());
            }                
        }
        if (IsMoveUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _startPos) < 0.1f)
            {
                IsMoveUp = false;
                IsFollow = false;
                _IsAttack = false;
            }
        }
    }

    private void FollowPlayer()
    {
        if (IsFollow)
        {
            Vector3 playerPos = _player.transform.position + _TargetOffset;
            transform.position = playerPos;
            _startPos = transform.position + _ReturnOffset;
        }
    }
}
