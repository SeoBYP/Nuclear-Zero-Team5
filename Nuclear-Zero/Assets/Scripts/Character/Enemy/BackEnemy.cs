using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEnemy : EnemyController
{
    public override void Init()
    {
        base.Init();
        //xIndex;
    }

    protected override void Run()
    {
        base.Run();
        Move();
    }

    private void Move()
    {
        Vector2 curPos = transform.position;
        Vector2 targetPos = Vector2.right * xIndex * speed * Time.deltaTime;
        transform.position = curPos + targetPos;
    }
    protected override void Damaged() { _player.Dead(); }
}
