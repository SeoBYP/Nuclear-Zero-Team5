using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : EnemyController
{
    public override void Init()
    {
        base.Init();
    }

    protected override void Run()
    {
        base.Run();

        Vector2 curPos = transform.position;
        Vector2 targetPos = Vector2.left * xIndex * speed * Time.deltaTime;
        transform.position = curPos + targetPos;
    }

    protected override void Damaged() { _player.TakeDamage(); }
}
