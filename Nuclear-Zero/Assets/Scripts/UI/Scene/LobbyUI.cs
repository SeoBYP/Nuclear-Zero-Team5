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
    }

    private bool _isOpenMenu;

    private Animation ShopAni;
    private Animation InfoAni;
    private Animation SettingAni;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        SetButtons();

        _isOpenMenu = false;

        BindEvent(GetButton((int)Buttons.Play).gameObject, OnPlay, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Menu).gameObject, OnMenu, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Tutorial).gameObject, OnTutorial, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Sns).gameObject, OnSns, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Shop).gameObject, OnShop, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Setting).gameObject, OnSetting, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Information).gameObject, OnInfomation, UIEvents.Click);
    }

    private void SetButtons()
    {
        GetButton((int)Buttons.Shop).gameObject.SetActive(false);
        GetButton((int)Buttons.Setting).gameObject.SetActive(false);
        GetButton((int)Buttons.Information).gameObject.SetActive(false);

        ShopAni = GetButton((int)Buttons.Shop).GetComponent<Animation>();
        InfoAni = GetButton((int)Buttons.Setting).GetComponent<Animation>();
        SettingAni = GetButton((int)Buttons.Information).GetComponent<Animation>();
    }

    private void OnPlay(PointerEventData data)
    {
        UIManager.Instance.ShowSceneUi<StageUI>();
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

        ShopAni.Play();
        InfoAni.Play();
        SettingAni.Play();

        _isOpenMenu = true;
    }

    private void CloseMenu()
    {
        GetButton((int)Buttons.Shop).gameObject.SetActive(false);
        GetButton((int)Buttons.Setting).gameObject.SetActive(false);
        GetButton((int)Buttons.Information).gameObject.SetActive(false);

        _isOpenMenu = false;
    }

    private void OnShop(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<ShopPopupUI>();
    }

    private void OnInfomation(PointerEventData data)
    {
        print("OnInfomation");
    }

    private void OnSetting(PointerEventData data)
    {
        print("OnSetting");
    }

    private void OnTutorial(PointerEventData data)
    {
        print("OnTutorial");
    }
    private void OnSns(PointerEventData data)
    {
        print("OnSns");
    }
}
