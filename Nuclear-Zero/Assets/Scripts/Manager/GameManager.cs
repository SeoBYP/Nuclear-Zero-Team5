using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Managers<GameManager>//,IUpdate
{
    private Joystick _joystick;
    private PlayerController _player;
    private MapController _map;

    public bool IsClear { get; set; }
    public bool IsGameOver { get; set; }
    public bool IsStart { get; set; }

    public override void Init()
    {
        base.Init();
        IsClear = false;
        IsGameOver = false;
        IsStart = false;
        _map = Utils.FindObjectOfType<MapController>(false);
    }

    public void GameStart()
    {
        GameScene.Start();

        _joystick = Utils.FindObjectOfType<Joystick>(true);
        _player = Utils.FindObjectOfType<PlayerController>(true);
        _map = Utils.FindObjectOfType<MapController>(false);

        EnemyController.IsStart = true;
        IsStart = true;
        IsClear = false;
        IsGameOver = false;
    }

    public void GameClear()
    {
        IsClear = true;
        EnemyController.IsStart = false;
        UIManager.Instance.ShowPopupUi<GameClearPopupUI>();
    }

    public void GameOver()
    {
        IsGameOver = true;
        EnemyController.IsStart = false;
        UIManager.Instance.ShowPopupUi<ResurrectionPopupUI>();
    }

    private void Update()
    {
        if (_player == null)
            return;
        if (IsClear || IsGameOver)
            return;

        //_player.Move(_joystick.Dir);
        _map.MapMove();
    }
}
