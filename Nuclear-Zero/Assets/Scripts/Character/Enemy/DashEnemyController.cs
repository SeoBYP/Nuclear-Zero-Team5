using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemyController : EnemyController
{
    public float speed;

    protected override void Init()
    {
        base.Init();
    }

    protected override void Run()
    {
        DoDash();
    }

    private void DoDash()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(-1f, 0, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }
}
