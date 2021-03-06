using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Managers<GameManager>//,IUpdate
{
    private Joystick _joystick;
    private PlayerController _player;

    public bool IsClear { get; set; }
    public bool IsGameOver { get; set; }
    public bool IsStart { get; set; }

    private bool IsPause;
    public override void Init()
    {
        base.Init();
        IsClear = false;
        IsGameOver = false;
        IsStart = false;
        IsPause = false;
    }

    public void GameStart()
    {
        UIManager.Instance.ShowSceneUi<GameUI>();
        _player = Utils.FindObjectOfType<PlayerController>(true);
        _joystick = Utils.FindObjectOfType<Joystick>(false);

        IsStart = true;
        IsClear = false;
        IsGameOver = false;
    }

    public void GameClear()
    {
        IsClear = true;
        UIManager.Instance.ShowPopupUi<GameClearPopupUI>();
        ClearGameObjects();
    }

    public void GameOver()
    {
        IsGameOver = true;
        UIManager.Instance.ShowPopupUi<ResurrectionPopupUI>();
        ClearGameObjects();
    }

    public void GamePuase()
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

    public void LoadGameMap()
    {
        int selectStage = DataManager.Instance.playerInfo.SelectStage;
        string stageName = DataManager.Instance.playerInfo.GetPlayerStages(selectStage).StageName;
        //ResourcesManager.Instance.Instantiate($"Map/Stages/Stage1-2");
    }

    //public void InitGameObjects()
    //{
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    foreach (GameObject go in enemies)
    //    {
    //        go.GetComponent<EnemyController>().Init();
    //    }

    //}


    public void ClearGameObjects()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in enemies)
        {
            go.GetComponent<EnemyController>().Clear();
        }
    }

    private void Update()
    {
        if (_player == null)
            return;
        if (IsClear || IsGameOver)
            return;
        _player.Move(_joystick.Direction);
        //_map.MapMove();
    }
}
