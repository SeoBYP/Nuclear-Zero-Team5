using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : BlockController
{
    [SerializeField] private float jumpPower;
    protected override void Init()
    {
        base.Init();
    }

    public override void OnSteped()
    {
        base.OnSteped();
        SetJump();
    }


    private void SetJump()
    {
        if (_player != null)
        {
            _player.Jump(jumpPower);
        }
    }
}
