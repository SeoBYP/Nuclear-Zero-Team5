using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class StageCharacterSelectPopupUI : PopupUI
{
    enum Buttons
    {
        //ClosePopup,
        StartStage,
        //SelectCharacterBtn,
    }

    enum Texts
    {
        SelectStageText
    }

    enum GameObjects
    {
        BackGround,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.StartStage).gameObject, OnStartStage, UIEvents.Click);
        //BindEvent(GetButton((int)Buttons.SelectCharacterBtn).gameObject, OnSelectCharacterBtn, UIEvents.Click);
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClosePopup, UIEvents.Click);
    }

    public void SetSeleteStageText(string texts)
    {
        GetText((int)Texts.SelectStageText).text = texts;
    }

    private void OnClosePopup(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void OnStartStage(PointerEventData data)
    {
        SceneManagerEx.Instance.LoadScene(Scene.Game);
    }

    private void OnSelectCharacterBtn(PointerEventData data)
    {
        print("캐릭터 인벤토리 창을 연다.");
    }
}
