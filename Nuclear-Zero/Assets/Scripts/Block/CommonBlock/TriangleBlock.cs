using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBlock : BlockController
{
    protected override void Init()
    {
        base.Init();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerTakeDamage();
    }

    //public override void OnSteped()
    //{
    //    base.OnSteped();
        
    //}

    private void PlayerTakeDamage()
    {
        if (_player != null)
            _player.TakeDamage();
    }
}
