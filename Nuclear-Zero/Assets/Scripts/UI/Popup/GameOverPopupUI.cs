using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class GameOverPopupUI : PopupUI
{
    private int timer;

    enum Buttons
    {
        Exit,
        Replay,
    }

    enum Texts
    {
        CoinText,
    }

    private bool IsExit = false;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Replay).gameObject, OnReplay, UIEvents.Click);
        SetPlayerStageClear();
    }

    private void SetPlayerStageClear()
    {
        //int stageIndex = DataManager.Instance.playerInfo.SelectStage;
        GameUI gameUI = UIManager.Instance.Get<GameUI>();
        int coin = gameUI.CoinCount;
        GetText((int)Texts.CoinText).text = coin.ToString();
        DataManager.Instance.playerInfo.SetCoin(coin);
    }

    private void OnExit(PointerEventData data)
    {
        GameManager.Instance.OpenStagePopup = true;
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }

    private void OnReplay(PointerEventData data)
    {
        GameManager.Instance.OpenStagePopup = false;
        SceneManagerEx.Instance.ReLoadScene(Scene.Game);
    }
}
