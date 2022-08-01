using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBlock : BlockController
{
    private bool _isEffected = false;
    [SerializeField] private float speed;
    protected override void Init()
    {
        base.Init();
    }

    public override void OnSteped()
    {
        base.OnSteped();
        SetSlow();
    }

    private void SetSlow()
    {
        if (_player != null)
        {
            _player.SetFast(speed);
            print("Player Slow");
        }
    }
}
