using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBlock : BlockController
{
    protected override void Init()
    {
        base.Init();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    PlayerTakeDamage();
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerTakeDamage();
    }

    private void PlayerTakeDamage()
    {
        if (_player != null)
            _player.TakeDamage();
    }
}
