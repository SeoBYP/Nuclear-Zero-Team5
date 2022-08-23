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

    public override void Init()
    {
        base.Init();
        Binds();
        GameAudioManager.Instance.StopBackGround();
        GameAudioManager.Instance.Play2DSound("Defeate");
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
        GetText((int)Texts.CoinText).text = $"+ {coin}";
        DataManager.Instance.playerInfo.SetCoin(coin);
    }

    private void OnExit(PointerEventData data)
    {
        //GameAudioManager.Instance.PlayBackGround("LobbyBGM");
        GameManager.Instance.OpenStagePopup = true;
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }

    private void OnReplay(PointerEventData data)
    {
        GameManager.Instance.OpenStagePopup = false;
        StageSelectPopupUI popupUI = UIManager.Instance.ShowPopupUi<StageSelectPopupUI>();
        if (popupUI != null)
        {
            popupUI.SetSeleteStageText(DataManager.Instance.playerInfo.SelectStage);
        }
        //SceneManagerEx.Instance.ReLoadScene(Scene.Game);
    }
}
