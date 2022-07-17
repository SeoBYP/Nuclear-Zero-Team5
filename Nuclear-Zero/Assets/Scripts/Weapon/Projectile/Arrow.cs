using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile<Arrow>
{
    public float speed;

    public override void Init()
    {
        base.Init();
    }

    public override void Excute()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(-1f, 0, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }
}
