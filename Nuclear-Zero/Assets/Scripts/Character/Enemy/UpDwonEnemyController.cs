using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDwonEnemyController : EnemyController
{
    public float speed;
    public float _yIndex;
    public override void Init()
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
        Vector3 nextPos = new Vector3(-1f, _yIndex, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            if (collision.gameObject.name == "Buttom" || collision.gameObject.name == "Top")
            {
                _yIndex *= -1;
            }
        }
    }
}
