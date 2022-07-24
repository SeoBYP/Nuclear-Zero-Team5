using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBlock : BlockController
{
    private GameUI _gameUI;
    private bool _isEffected = false;
    private SpriteRenderer _sprite;
    protected override void Init()
    {
        base.Init();
        _gameUI = UIManager.Instance.Get<GameUI>();
        _sprite = GetComponent<SpriteRenderer>();
        _isEffected = false;
    }

    public override void OnSteped()
    {
        base.OnSteped();
        AddLife();
    }

    private void AddLife()
    {
        if (_isEffected == false)
        {
            if (_gameUI == null)
                _gameUI = UIManager.Instance.Get<GameUI>();
            _gameUI.AddHeart();
            _isEffected = true;
            _sprite.color = Color.gray;
        }
    }
}
