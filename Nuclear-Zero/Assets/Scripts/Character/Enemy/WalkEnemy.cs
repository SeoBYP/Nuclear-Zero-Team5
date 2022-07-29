using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyController
{
    public float AttackRange;
    private bool _IsAttack;
    public override void Init()
    {
        base.Init();
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _IsAttack = false;
    }

    protected override void Run()
    {
        base.Run();
        CheckPlayer();
        if (_IsAttack)
        {
            Vector2 curPos = transform.position;
            Vector2 targetPos = Vector2.left * xIndex * speed * Time.deltaTime;
            transform.position = curPos + targetPos;
        }
    }

    private void CheckPlayer()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < AttackRange)
        {
            _IsAttack = true;
        }
    }
    public override void Damaged() { _player.TakeDamage(); }
}
