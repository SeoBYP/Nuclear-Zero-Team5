using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class LobbyUI : SceneUI
{
    enum Buttons
    {
        Play,
        Menu,
        Tutorial,
        Sns,
        Shop,
        Information,
        Setting,
        Character,
        Story,
    }

    enum Texts
    {
        GoldText,
    }

    private bool _isOpenMenu;

    private Animation ShopAni;
    private Animation InfoAni;
    private Animation SettingAni;
    private Animation SnsAni;
    private Animation StoryAni;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        SetButtons();

        _isOpenMenu = false;
        SetPlayerGoldText();
        BindEvent(GetButton((int)Buttons.Play).gameObject, OnPlay, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Menu).gameObject, OnMenu, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Tutorial).gameObject, OnTutorial, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Sns).gameObject, OnSns, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Shop).gameObject, OnShop, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Setting).gameObject, OnSetting, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Information).gameObject, OnInfomation, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Character).gameObject, OnCharacter, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Story).gameObject, OnStory, UIEvents.Click);
    }

    private void SetPlayerGoldText()
    {
        GetText((int)Texts.GoldText).text = DataManager.Instance.playerInfo.Gold.ToString();
    }

    private void SetButtons()
    {
        GetButton((int)Buttons.Shop).gameObject.SetActive(false);
        GetButton((int)Buttons.Setting).gameObject.SetActive(false);
        GetButton((int)Buttons.Information).gameObject.SetActive(false);
        GetButton((int)Buttons.Sns).gameObject.SetActive(false);
        GetButton((int)Buttons.Story).gameObject.SetActive(false);

        ShopAni = GetButton((int)Buttons.Shop).GetComponent<Animation>();
        InfoAni = GetButton((int)Buttons.Setting).GetComponent<Animation>();
        SettingAni = GetButton((int)Buttons.Information).GetComponent<Animation>();
        SnsAni = GetButton((int)Buttons.Sns).GetComponent<Animation>();
        StoryAni = GetButton((int)Buttons.Story).GetComponent<Animation>();
    }

    private void OnPlay(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<StagePopupUI>();
        //SceneManagerEx.Instance.LoadScene(Scene.Stage);
    }
    private void OnMenu(PointerEventData data)
    {
        if (_isOpenMenu == false)
            OpenMenu();
        else
            CloseMenu();
    }

    private void OpenMenu()
    {
        GetButton((int)Buttons.Shop).gameObject.SetActive(true);
        GetButton((int)Buttons.Setting).gameObject.SetActive(true);
        GetButton((int)Buttons.Information).gameObject.SetActive(true);
        GetButton((int)Buttons.Sns).gameObject.SetActive(true);
        GetButton((int)Buttons.Story).gameObject.SetActive(true);

        ShopAni.Play();
        InfoAni.Play();
        SettingAni.Play();
        SnsAni.Play();
        StoryAni.Play();

        _isOpenMenu = true;
    }

    private void CloseMenu()
    {
        GetButton((int)Buttons.Shop).gameObject.SetActive(false);
        GetButton((int)Buttons.Setting).gameObject.SetActive(false);
        GetButton((int)Buttons.Information).gameObject.SetActive(false);
        GetButton((int)Buttons.Sns).gameObject.SetActive(false);
        GetButton((int)Buttons.Story).gameObject.SetActive(false);

        _isOpenMenu = false;
    }

    private void OnShop(PointerEventData data)
    {
        print("OnShop");
        //UIManager.Instance.ShowPopupUi<ShopPopupUI>();
    }

    private void OnInfomation(PointerEventData data)
    {
        print("OnInfomation");
    }

    private void OnSetting(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<SettingPopupUI>();
    }

    private void OnTutorial(PointerEventData data)
    {
        print("OnTutorial");
    }
    private void OnSns(PointerEventData data)
    {
        print("OnSns");
    }
    private void OnCharacter(PointerEventData data)
    {
        print("OnCharacter");
    }
    private void OnStory(PointerEventData data)
    {
        print("OnStory");
    }
}
