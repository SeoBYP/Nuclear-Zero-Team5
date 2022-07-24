using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBlock : BlockController
{
    protected override void Init()
    {
        base.Init();
    }

    public override void OnSteped()
    {
        base.OnSteped();
        PlayerTakeDamage();
    }

    private void PlayerTakeDamage()
    {
        if (_player != null)
            _player.TakeDamage();
    }
}
