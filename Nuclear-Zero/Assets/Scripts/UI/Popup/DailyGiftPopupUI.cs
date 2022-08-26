using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class DailyGiftPopupUI : PopupUI
{
    enum Buttons
    {
        GetReward,
        GetGift,
    }

    enum GameObjects
    {
        BackGround,
    }

    public override void Init()
    {
        base.Init();
        GoogleMobileAdsManager.Instance.RequestAndLoadRewardedInterstitialAd();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.GetReward).gameObject, OnClose, UIEvents.Click);
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GetGift).gameObject, OnGetGift, UIEvents.Click);
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void OnGetGift(PointerEventData data)
    {
        UIManager.Instance.Get<LobbyUI>().GetDailyGift();
        GetButton((int)Buttons.GetGift).gameObject.SetActive(false);
        GoogleMobileAdsManager.Instance.ShowRewardedInterstitialAd();
        
    }

    public void OnReward(int reward)
    {
        DataManager.Instance.playerInfo.DailyGift = true;
    }
}
