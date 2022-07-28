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
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClosePopup, UIEvents.Click);
    }

    public void SetSeleteStageText(string texts)
    {
        GetText((int)Texts.SelectStageText).text = texts;
        SetPlayerSelectStage(texts);
    }

    private void SetPlayerSelectStage(string stage)
    {
        switch (stage)
        {
            case "Stage1-1":
                GameData.dialogueObjectName = "Chapter1";
                print(GameData.dialogueObjectName);
                break;
            case "Stage2-1":
                GameData.dialogueObjectName = "Chapter2";
                print(GameData.dialogueObjectName);
                break;
            case "Stage3-1":
                GameData.dialogueObjectName = "Chapter3";
                print(GameData.dialogueObjectName);
                break;
            case "Stage4-1":
                GameData.dialogueObjectName = "Chapter4";
                print(GameData.dialogueObjectName);
                break;
            default:
                GameData.dialogueObjectName = string.Empty;
                break;
        }
    }

    private string StageToString(Stage stage)
    {
        return stage.ToString();
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
