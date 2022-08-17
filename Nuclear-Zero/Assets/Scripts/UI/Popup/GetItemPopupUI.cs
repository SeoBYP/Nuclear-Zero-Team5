using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class GetItemPopupUI : PopupUI
{
    enum Buttons
    {
        ADButton,
    }

    enum GameObjects
    {
        BackGround,
    }

    enum Texts
    {
        CoinCount,
    }

    private int coin;

    public override void Init()
    {
        base.Init();
        Binds();
        GoogleMobileAdsManager.Instance.RequestAndLoadRewardedInterstitialAd();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.ADButton).gameObject, OnADButton, UIEvents.Click);
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClose, UIEvents.Click);
        SetReward();
    }

    private void SetReward()
    {
        coin = Random.Range(150, 250);
        GetText((int)Texts.CoinCount).text = $"+ {coin}";
        DataManager.Instance.playerInfo.SetCoin(coin);
    }

    public void SetPlayerStageClear(int reward)
    {
        coin *= reward;
        GetText((int)Texts.CoinCount).text = $"+ {coin}";
        print("GetReward");
    }

    private void OnADButton(PointerEventData data)
    {
        GoogleMobileAdsManager.Instance.ShowRewardedInterstitialAd();

    }
    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }
}
