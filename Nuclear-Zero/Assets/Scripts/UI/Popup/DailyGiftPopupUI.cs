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
        Daily,
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

        CheckPlayerADRemover();
        
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Daily).gameObject, OnGetGift, UIEvents.Click);
    }

    private void CheckPlayerADRemover()
    {
        if (DataManager.Instance.playerInfo.ADRemove)
        {
            BindEvent(GetButton((int)Buttons.GetReward).gameObject, OnGift, UIEvents.Click);
            GetButton((int)Buttons.Daily).gameObject.SetActive(false);
        }
        else
        {
            BindEvent(GetButton((int)Buttons.GetReward).gameObject, OnClose, UIEvents.Click);
            GetButton((int)Buttons.Daily).gameObject.SetActive(true);
        }
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void OnGift(PointerEventData data)
    {
        OnReward(2);
    }

    private void OnGetGift(PointerEventData data)
    {
        GetButton((int)Buttons.Daily).gameObject.SetActive(false);
        GoogleMobileAdsManager.Instance.ShowRewardedInterstitialAd();
    }

    public void OnReward(int reward)
    {
        DataManager.Instance.playerInfo.DailyGift = true;
        ClosePopupUI();
        UIManager.Instance.ShowPopupUi<GetDailyGiftPopupUI>();
        UIManager.Instance.Get<LobbyUI>().GetDailyGift();
    }
}
