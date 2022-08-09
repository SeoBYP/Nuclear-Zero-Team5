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
        ADButton,
    }

    enum Texts
    {
        RewardCoinText,
    }

    private ResultStar[] resultStars;

    public override void Init()
    {
        base.Init();
        Binds();
        GoogleMobileAdsManager.Instance.RequestAndLoadRewardedInterstitialAd();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        SetPlayerStageClear();

        BindEvent(GetButton((int)Buttons.NextStage).gameObject, OnNextStage, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ADButton).gameObject, OnADButtons, UIEvents.Click);
        FindResultStars();
        // ???? ?????????? ?????? ???????? ???? ?????? ????????????,
        // ???? ?????????? ?????? ???????? ???? ?????? ????????????????.
        if (DataManager.Instance.playerInfo.SelectStage < DataManager.Instance.playerInfo.PlayerStages.Count)
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
        GetText((int)Texts.RewardCoinText).text = coin.ToString();
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

    private void OnADButtons(PointerEventData data)
    {
        GoogleMobileAdsManager.Instance.ShowRewardedInterstitialAd();
    }

    private void OnNextStage(PointerEventData data)
    {
        ++DataManager.Instance.playerInfo.SelectStage;
        SceneManagerEx.Instance.ReLoadScene(Scene.Game);
    }

    private void OnExit(PointerEventData data)
    {
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }
}
