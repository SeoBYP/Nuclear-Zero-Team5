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
        Exit,
        ADButton,
        GetReward,
    }

    enum Texts
    {
        RewardCoinText,
    }

    private ResultStar[] resultStars;

    private int stageIndex;
    private int star;
    private int coin;

    public override void Init()
    {
        base.Init();
        Binds();
        GameAudioManager.Instance.Play2DSound("Victory");
        GoogleMobileAdsManager.Instance.RequestAndLoadRewardedInterstitialAd();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        FindResultStars();

        SetPlayerStageClear();

        //BindEvent(GetButton((int)Buttons.NextStage).gameObject, OnNextStage, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ADButton).gameObject, OnADButtons, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GetReward).gameObject, OnExit, UIEvents.Click);
        //if (DataManager.Instance.playerInfo.SelectStage < DataManager.Instance.playerInfo.PlayerStages.Count)
        //    GetButton((int)Buttons.NextStage).gameObject.SetActive(true);
        //else
        //    GetButton((int)Buttons.NextStage).gameObject.SetActive(false);
    }

    private void SetPlayerStageClear()
    {
        stageIndex = DataManager.Instance.playerInfo.SelectStage;
        GameUI gameUI = UIManager.Instance.Get<GameUI>();
        star = gameUI.StarCount;
        SetResultStars(star);
        coin = gameUI.CoinCount * 5;
        GetText((int)Texts.RewardCoinText).text = $"+ {coin}";
    }

    public void SetPlayerStageClear(int reward)
    {
        GetButton((int)Buttons.ADButton).gameObject.SetActive(false);
        stageIndex = DataManager.Instance.playerInfo.SelectStage;
        GameUI gameUI = UIManager.Instance.Get<GameUI>();
        star = gameUI.StarCount;
        SetResultStars(star);
        coin = gameUI.CoinCount * 5 * reward;
        GetText((int)Texts.RewardCoinText).text = $"+ {coin}";
    }

    private void SetResultStars(int index)
    {
        for(int i = 0; i < resultStars.Length; i++)
        {
            resultStars[i].SetDeShineStar();
        }
        for(int i = 0; i < index; i++)
        {
            resultStars[i].SetShineStar();
        }
    }

    private void FindResultStars()
    {
        resultStars = GetComponentsInChildren<ResultStar>();
        for (int i = 0; i < resultStars.Length; i++)
        {
            resultStars[i].Init();
        }
    }

    private void OnADButtons(PointerEventData data)
    {
        GoogleMobileAdsManager.Instance.ShowRewardedInterstitialAd();
        
    }

    //private void OnNextStage(PointerEventData data)
    //{
    //    DataManager.Instance.playerInfo.SetClearStage(stageIndex, star, coin);
    //    ++DataManager.Instance.playerInfo.SelectStage;
    //    GameManager.Instance.OpenStagePopup = false;
    //    SceneManagerEx.Instance.ReLoadScene(Scene.Game);
    //}

    private void OnExit(PointerEventData data)
    {
        DataManager.Instance.playerInfo.SetClearStage(stageIndex, star, coin);
        GameManager.Instance.OpenStagePopup = true;
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }
}
