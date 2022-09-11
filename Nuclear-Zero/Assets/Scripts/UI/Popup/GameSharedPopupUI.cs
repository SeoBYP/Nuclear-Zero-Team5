using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class GameSharedPopupUI : PopupUI
{
    enum Buttons
    {
        Close,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
        SetReward();
    }

    private void SetReward()
    {
        DataManager.Instance.playerInfo.Share = true;
        DataManager.Instance.playerInfo.SetCoin(4000);
        DataManager.Instance.playerInfo.SetShieldItemCount(1);
        DataManager.Instance.playerInfo.SetMagnetItemCount(1);
        DataManager.Instance.playerInfo.SetLifeItemCount(1);
        UIManager.Instance.Get<LobbyUI>().SetPlayerGoldText();
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }
}
