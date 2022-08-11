using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile<Arrow>
{
    public float speed;
    private Vector2 _targetdir;

    public override void Init()
    {
        base.Init();
    }

    public void SetTargetDir(Vector2 dir) { _targetdir = dir.normalized; }

    public override void Excute()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = _targetdir * speed * Time.deltaTime;
        //transform.LookAt(_targetdir);
        //transform.rotation = Quaternion.Euler();
        transform.position = curPos + nextPos;
    }
}
