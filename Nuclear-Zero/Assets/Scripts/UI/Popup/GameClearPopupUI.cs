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

    private GameResultStar[] resultStars;
    [SerializeField] float _delayTime;
    private int stageIndex;
    private int star;
    private int coin;

    [SerializeField] int _testCount;

    public override void Init()
    {
        base.Init();
        Binds();
        GameAudioManager.Instance.StopBackGround();
        GameAudioManager.Instance.Play2DSound("Victory");
        GoogleMobileAdsManager.Instance.RequestAndLoadRewardedInterstitialAd();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        FindResultStars();

        SetPlayerStageClear();
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ADButton).gameObject, OnADButtons, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GetReward).gameObject, OnExit, UIEvents.Click);
    }

    private void SetPlayerStageClear()
    {
        stageIndex = DataManager.Instance.playerInfo.SelectStage;
        GameUI gameUI = UIManager.Instance.Get<GameUI>();
        star = gameUI.StarCount;
        StartCoroutine(SetResultStars(star));
        coin = gameUI.CoinCount * 5;
        GetText((int)Texts.RewardCoinText).text = $"+ {coin}";
    }

    public void SetPlayerStageClear(int reward)
    {
        GetButton((int)Buttons.ADButton).gameObject.SetActive(false);
        stageIndex = DataManager.Instance.playerInfo.SelectStage;
        GameUI gameUI = UIManager.Instance.Get<GameUI>();
        star = gameUI.StarCount;
        //StartCoroutine(SetResultStars(star));
        coin = gameUI.CoinCount * 5 * reward;
        GetText((int)Texts.RewardCoinText).text = $"+ {coin}";
    }

    IEnumerator SetResultStars(int index)
    {
        for(int i = 0; i < resultStars.Length; i++)
        {
            resultStars[i].Deactive();
        }
        for(int i = 0; i < resultStars.Length; i++)
        {
            yield return YieldInstructionCache.WaitForSeconds(_delayTime);
            if (i < index)
                resultStars[i].SetShineStar();
            else
                resultStars[i].SetDeShineStar();
            
        }
        yield return null;
    }

    private void FindResultStars()
    {
        resultStars = GetComponentsInChildren<GameResultStar>();
        for (int i = 0; i < resultStars.Length; i++)
        {
            resultStars[i].Init();
        }
    }

    private void OnADButtons(PointerEventData data)
    {
        GoogleMobileAdsManager.Instance.ShowRewardedInterstitialAd();
        
    }

    private void OnExit(PointerEventData data)
    {
        if (DataManager.Instance.playerInfo.SelectStage == 16)
            GameManager.Instance._ShowEndding = true;
        DataManager.Instance.playerInfo.SetClearStage(stageIndex, star, coin);
        GameManager.Instance.OpenStagePopup = true;
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }
}
