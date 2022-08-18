using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBlock : BlockController
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
        SetFast();
    }

    private void SetFast()
    {
        if(_player != null)
        {
            GameAudioManager.Instance.Play2DSound("Fast");
            _player.SetFast(speed);
        }
    }
}
