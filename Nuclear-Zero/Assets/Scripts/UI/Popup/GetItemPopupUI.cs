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
        GetReward,
        GetGift,
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
        BindEvent(GetButton((int)Buttons.GetReward).gameObject, OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GetGift).gameObject, OnGetGift, UIEvents.Click);
        SetReward();
        CheckPlayerADRemover();
    }

    private void SetReward()
    {
        coin = Random.Range(150, 250);
        GetText((int)Texts.CoinCount).text = $"+ {coin}";
        DataManager.Instance.playerInfo.SetCoin(coin);
    }

    public void SetPlayerStageClear(int reward)
    {
        GetButton((int)Buttons.ADButton).gameObject.SetActive(false);
        coin *= reward;
        GetText((int)Texts.CoinCount).text = $"+ {coin}";
        print("GetReward");
    }

    private void CheckPlayerADRemover()
    {
        if (DataManager.Instance.playerInfo.ADRemove)
        {
            GetButton((int)Buttons.ADButton).gameObject.SetActive(false);
            GetButton((int)Buttons.GetReward).gameObject.SetActive(false);
        }
        else
        {
            GetButton((int)Buttons.GetGift).gameObject.SetActive(false);
        }
    }
    private bool _isStart = false;
    private IEnumerator GetReward()
    {
        _isStart = true;
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        UIManager.Instance.Get<LobbyUI>().SetPlayerGoldText();
        ClosePopupUI();
        _isStart = false;
    }

    private void OnGetGift(PointerEventData data)
    {
        if (_isStart)
            return;
        SetPlayerStageClear(2);
        StartCoroutine(GetReward());
    }

    private void OnADButton(PointerEventData data)
    {
        GoogleMobileAdsManager.Instance.ShowRewardedInterstitialAd();
    }
    private void OnClose(PointerEventData data)
    {
        UIManager.Instance.Get<LobbyUI>().SetPlayerGoldText();
        ClosePopupUI();
    }
}
