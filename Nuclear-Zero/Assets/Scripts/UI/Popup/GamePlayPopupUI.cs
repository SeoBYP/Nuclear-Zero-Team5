using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class GamePlayPopupUI : PopupUI
{
    enum Buttons
    {
        Play,
    }

    enum Texts
    {
        StageText,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        BindEvent(GetButton((int)Buttons.Play).gameObject, OnPlay, UIEvents.Click);
        SetStageName();
    }

    private void SetStageName()
    {
        int selectStage = DataManager.Instance.playerInfo.SelectStage;
        string stageName = DataManager.Instance.playerInfo.GetPlayerStages(selectStage).StageName;
        GetText((int)Texts.StageText).text = stageName;
    }

    private void OnPlay(PointerEventData data)
    {
        if(GameManager.Instance.IsStart == false)
        {
            GameManager.Instance.GameStart();
            ClosePopupUI();
        }

    }
}
