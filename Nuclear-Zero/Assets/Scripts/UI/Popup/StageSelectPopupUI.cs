using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class StageSelectPopupUI : PopupUI
{
    enum Buttons
    { 
        StartStage,
    }

    enum Texts
    {
        SelectStageText,
        ShieldCount,
        MagnetCount,
        LifeCount,
    }

    enum GameObjects
    {
        BackGround,
    }

    enum Toggles
    {
        ShieldToggle,
        MagnetToggle,
        LifeToggle,
    }

    public override void Init()
    {
        base.Init();
        GameManager.Instance.SetDefaultItem();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Toggle>(typeof(Toggles));

        CheckPlayerItemsCount();
        SetDefualtToggle();

        GetText((int)Texts.ShieldCount).text = DataManager.Instance.playerInfo.ShieldItem.ToString();
        GetText((int)Texts.MagnetCount).text = DataManager.Instance.playerInfo.MagnetItem.ToString();
        GetText((int)Texts.LifeCount).text = DataManager.Instance.playerInfo.LifeItem.ToString();

        BindEvent(GetButton((int)Buttons.StartStage).gameObject, OnStartStage, UIEvents.Click);
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClosePopup, UIEvents.Click);
    }

    private void CheckPlayerItemsCount()
    {
        if (DataManager.Instance.playerInfo.ShieldItem == 0)
            Get<Toggle>((int)Toggles.ShieldToggle).enabled = false;
        else
            Get<Toggle>((int)Toggles.ShieldToggle).onValueChanged.AddListener(OnSheildToggle);


        if (DataManager.Instance.playerInfo.MagnetItem == 0)
            Get<Toggle>((int)Toggles.MagnetToggle).enabled = false;
        else
            Get<Toggle>((int)Toggles.MagnetToggle).onValueChanged.AddListener(OnMagnetToggle);


        if (DataManager.Instance.playerInfo.LifeItem == 0)
            Get<Toggle>((int)Toggles.LifeToggle).enabled = false;
        else
            Get<Toggle>((int)Toggles.LifeToggle).onValueChanged.AddListener(OnLifeToggle);
    }

    private void SetDefualtToggle()
    {
        Get<Toggle>((int)Toggles.ShieldToggle).isOn = GameManager.Instance._shield;
        Get<Toggle>((int)Toggles.MagnetToggle).isOn = GameManager.Instance._magnet;
        Get<Toggle>((int)Toggles.LifeToggle).isOn = GameManager.Instance._life;
    }

    public void SetSeleteStageText(string texts,int stageindex)
    {
        GetText((int)Texts.SelectStageText).text = texts;
        SetPlayerSelectStage(stageindex);
    }

    private void SetPlayerSelectStage(int stageIndex)
    {
        switch (stageIndex)
        {
            case 1:
                DataManager.Instance.playerInfo.DialogueObjectName = "Chapter1";
                break;
            case 5:
                DataManager.Instance.playerInfo.DialogueObjectName = "Chapter2";
                break;
            case 9:
                DataManager.Instance.playerInfo.DialogueObjectName = "Chapter3";
                break;
            case 13:
                DataManager.Instance.playerInfo.DialogueObjectName = "Chapter4";
                break;
            default:
                DataManager.Instance.playerInfo.DialogueObjectName = string.Empty;
                break;
        }
    }

    private void OnClosePopup(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void SetItemCount()
    {
        if (Get<Toggle>((int)Toggles.ShieldToggle).isOn)
            DataManager.Instance.playerInfo.SetShieldItemCount(-1);
        if (Get<Toggle>((int)Toggles.MagnetToggle).isOn)
            DataManager.Instance.playerInfo.SetMagnetItemCount(-1);
        if (Get<Toggle>((int)Toggles.LifeToggle).isOn)
            DataManager.Instance.playerInfo.SetLifeItemCount(-1);
    }

    private void OnStartStage(PointerEventData data)
    {
        SetItemCount();
        SceneManagerEx.Instance.LoadScene(Scene.Game);
    }

    private void OnSheildToggle(bool state)
    {
        GameAudioManager.Instance.Play2DSound("Touch");
        GameManager.Instance._shield = state;
    }
    private void OnMagnetToggle(bool state)
    {
        GameAudioManager.Instance.Play2DSound("Touch");
        GameManager.Instance._magnet = state;
    }
    private void OnLifeToggle(bool state)
    {
        GameAudioManager.Instance.Play2DSound("Touch");
        GameManager.Instance._life = state;
    }
}
