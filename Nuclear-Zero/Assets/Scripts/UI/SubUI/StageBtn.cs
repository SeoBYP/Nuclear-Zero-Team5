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

    private Button _button;
    private Chapter _chapter;
    private Action<string> onClick;
    private int _stageCount;
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
            onClick($"Stage{(int)_chapter + 1}-{_stageCount}");
        }
    }

    public void SetButtonInfo(Chapter chapter,int stageCount,bool clear, Action<string> eventFunc, bool currently = false)
    {
        onClick = eventFunc;
        _chapter = chapter;
        _stageCount = stageCount + 1;
        if (clear)
        {
            SetActiveStar(3);
        }
        else
        {
            if (currently)
            {
                SetActiveStar(0);
            }
        }
    }

    private void SetActiveStar(int count)
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if(i < count)
            {
                stars[i].SetShineStar();
            }
            else
            {
                stars[i].SetDeShineStar();
            }
        }
    }
}
