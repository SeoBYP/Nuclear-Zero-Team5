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
        if(_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _IsAttack = false;
    }

    protected override void Run()
    {
        base.Run();
        if(_IsAttack == false)
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
        if (_player == null)
            _player = Utils.FindObjectOfType<PlayerController>();
        if (Vector2.Distance(transform.position, _player.transform.position) < AttackRange)
        {
            GameAudioManager.Instance.Play2DSound("Missile");
            _IsAttack = true;
        }
    }
    public override void Damaged() { _player.TakeDamage(); }
}
