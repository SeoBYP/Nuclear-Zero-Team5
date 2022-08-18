using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Managers<GameManager>//,IUpdate
{
    private PlayerController _player;

    private bool IsPause;
    public bool OpenStagePopup;

    public bool _shield = false;
    public bool _magnet = false;
    public bool _life = false;

    public override void Init()
    {
        base.Init();
        OpenStagePopup = false;
        _shield = false;
        _magnet = false;
        _life = false;
    }

    public void SetDefaultItem()
    {
        _shield = false;
        _magnet = false;
        _life = false;
    }

    public void GameStart()
    {
        UIManager.Instance.ShowSceneUi<GameUI>();
        _player = Utils.FindObjectOfType<PlayerController>(true);

        _player.SetDefaultGoalDistance();

        PlayerController.SetStart(true);
        EnemyController.SetStart(true);
    }

    public void GameClear()
    {
        UIManager.Instance.ShowPopupUi<GameClearPopupUI>();

        PlayerController.SetPause(true);
        EnemyController.SetPause(true);
    }

    public void GameOver()
    {
        UIManager.Instance.ShowPopupUi<GameOverPopupUI>();

        PlayerController.SetPause(true);
        EnemyController.SetPause(true);
    }
    
    public void GamePause()
    {
        if (IsPause == false)
        {
            Time.timeScale = 0;
            IsPause = true;
        }
        else
        {
            Time.timeScale = 1;
            IsPause = false;
        }
    }

    public void SetPlayerShieldItem()
    {

    }

    public void SetPlayerMagnetItem()
    {

    }

    public void SetPlayerLifeItem()
    {

    }

    public void LoadGameMap()
    {
        int selectStage = DataManager.Instance.playerInfo.SelectStage;
        string stageName = DataManager.Instance.playerInfo.GetPlayerStages(selectStage).StageName;
        GameObject go = ResourcesManager.Instance.Instantiate($"Map/Stages/{stageName}");
        if (go == null)
        {
            go = ResourcesManager.Instance.Instantiate($"Map/Stages/Default");
        }
    }
}
