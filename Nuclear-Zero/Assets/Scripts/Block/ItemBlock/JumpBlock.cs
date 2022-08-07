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

    private void OnTriggerStay2D(Collider2D collision)
    {
        SetJump();
    }

    public override void OnSteped()
    {
        base.OnSteped();
        //SetJump();
    }

    private void SetJump()
    {
        if (_player != null)
        {
            _player.Jump(jumpPower);
        }
    }
}
