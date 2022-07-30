using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
using System;
public class StageBtn : SubUI
{
    List<ResultStar> stars = new List<ResultStar>();

    [SerializeField] private int _stageIndex;
    public int StageIndex { get { return _stageIndex; } }
    private Button _button;
    private Action<string,int> onClick;
    private PlayerStages _curentStatge;
    public override void Init()
    {
        base.Init();

        Binds();
    }

    private void Binds()
    {
        _button = Utils.BindingFunc(transform, OnClickedButton);
        SetResultStars();
    }

    private void SetResultStars()
    {
        ResultStar[] results = GetComponentsInChildren<ResultStar>();
        for (int i = 0; i < results.Length; i++)
        {
            results[i].Init();
            results[i].SetDeShineStar();
            stars.Add(results[i]);
        }
    }

    private void OnClickedButton()
    {
        if (onClick != null)
        {
            DataManager.Instance.playerInfo.SelectStage = _curentStatge.StageIndex;
            onClick(_curentStatge.StageName,_stageIndex);
        }
    }

    public void SetButtonInfo(PlayerStages stages,Action<string,int> eventFunc)
    {
        _curentStatge = stages;
        onClick = eventFunc;
        if (_curentStatge.Cleared)
        {
            SetActiveStar();
        }
    }
    
    private void SetActiveStar()
    {
        foreach(ResultStar star in stars)
        {
            star.SetDeShineStar();
        }

        for (int i = 0; i < _curentStatge.ResultStar; i++)
        {
            stars[i].SetShineStar();
        }
    }
}
