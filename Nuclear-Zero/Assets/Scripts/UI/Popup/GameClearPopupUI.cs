using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class GameClearPopupUI : PopupUI
{
    enum Buttons
    {
        NextStage,
        Exit,
    }

    private ResultStar[] resultStars;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        SetPlayerStageClear();

        BindEvent(GetButton((int)Buttons.NextStage).gameObject, OnNextStage, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
        FindResultStars();
        // 다음 스테이지가 있다면 스테이지 이동 버튼을 활성화시키고,
        // 다음 스테이지가 없다면 스테이지 이동 버튼을 비활성화시킵니다.
        if (DataManager.Instance.playerInfo.SelectStage < DataManager.Instance.playerInfo.Stages.Count)
            GetButton((int)Buttons.NextStage).gameObject.SetActive(true);
        else
            GetButton((int)Buttons.NextStage).gameObject.SetActive(false);
    }

    private void SetPlayerStageClear()
    {
        int stageIndex = DataManager.Instance.playerInfo.SelectStage;
        GameUI gameUI = UIManager.Instance.Get<GameUI>();
        int star = gameUI.StarCount;
        int coin = gameUI.CoinCount;
        DataManager.Instance.playerInfo.SetClearStage(stageIndex,star,coin);
    }

    private void FindResultStars()
    {
        resultStars = GetComponentsInChildren<ResultStar>();
        for (int i = 0; i < resultStars.Length; i++)
        {
            resultStars[i].Init();
            resultStars[i].SetShineStar();
        }
    }

    private void OnNextStage(PointerEventData data)
    {
        //선택한 스테이지의 인덱스값을 증가시킵니다.
        ++DataManager.Instance.playerInfo.SelectStage;
        SceneManagerEx.Instance.ReLoadScene(Scene.Game);
    }

    private void OnExit(PointerEventData data)
    {
        SceneManagerEx.Instance.ReLoadScene(Scene.Lobby);
    }
}
