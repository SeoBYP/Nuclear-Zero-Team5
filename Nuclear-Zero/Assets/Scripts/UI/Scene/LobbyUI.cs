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
        Story,
        Plus,
        ExitGame,
        DailyGift,
    }

    enum Texts
    {
        GoldText,
    }

    enum Images
    {
        GetDailyGift,
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
        Bind<Image>(typeof(Images));
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
        BindEvent(GetButton((int)Buttons.Story).gameObject, OnStory, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Plus).gameObject, OnPlus, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ExitGame).gameObject, OnExitGame, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.DailyGift).gameObject, OnDailyGift, UIEvents.Click);
    }

    public void SetPlayerGoldText()
    {
        int gold = DataManager.Instance.playerInfo.Gold;
        string money = string.Empty;
        if (gold == 0)
            money = "0";
        else
            money = string.Format("{0:#,###}", gold);
        GetText((int)Texts.GoldText).text = money;
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
        UIManager.Instance.ShowPopupUi<ShopPopupUI>();
    }

    private void OnInfomation(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<InfomationPopupUI>();
    }

    private void OnSetting(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<SettingPopupUI>();
    }

    private void OnTutorial(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<TutorialPopupUI>();
    }
    private void OnSns(PointerEventData data)
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=100084511273164");
    }

    private void OnPlus(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<ShopPopupUI>().OpenCoinPackage();
    }

    private void OnStory(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<ReplayScenarioPopupUI>();
    }

    public void SetGiftBtn(bool _onTime)
    {
        if (_onTime)
        {
            if(DataManager.Instance.playerInfo.DailyGift == false)
                DontGetDailyGift();
            else
                GetDailyGift();
        }
        else
        {
            GetDailyGift();
        }
    }

    private void OnDailyGift(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<DailyGiftPopupUI>();
    }

    public void DontGetDailyGift()
    {
        GetButton((int)Buttons.DailyGift).gameObject.SetActive(true);
        GetImage((int)Images.GetDailyGift).gameObject.SetActive(false);
    }

    public void GetDailyGift()
    {
        GetButton((int)Buttons.DailyGift).gameObject.SetActive(false);
        GetImage((int)Images.GetDailyGift).gameObject.SetActive(true);
    }

    private void OnExitGame(PointerEventData data)
    {
        GPGSManager.Instance.SaveData();
        //GPGSBinder.Inst.Logout();
        Application.Quit();
    }
}
