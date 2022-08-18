using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : BlockController
{
    private bool _isEffected;
    private SpriteRenderer _sprite;

    protected override void Init()
    {
        base.Init();
        _isEffected = false;
        _sprite = GetComponent<SpriteRenderer>();
    }

    public override void OnSteped()
    {
        base.OnSteped();
        SetPlayerShield();
    }

    private void SetPlayerShield()
    {
        if(_isEffected == false)
        {
            GameAudioManager.Instance.Play2DSound("Shield");
            _player.Shieded();
            _isEffected = true;
            _sprite.color = Color.gray;
        }
    }
}
