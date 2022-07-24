using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class TalkTextPopupUI : PopupUI
{
    enum Texts
    {
        TalkText,
    }

    enum Images
    {
        CharacterImage1,
        CharacterImage2,
    }

    enum GameObjects
    {
        TalkBackGround,
    }

    private Color _activeCharacterColor = Color.white;
    private Color _deactiveCharacterColor = Color.gray;

    private int _index = 0;

    public override void Init()
    {
        base.Init();
        Binds();
        _index = 0;
    }

    private void Binds()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetGameObject(0), OnTouched, UIEvents.Click);
    }

    private void OnTouched(PointerEventData data)
    {
        if (_index == 0)
            NextText();
        else
            ClosePopupUI();
    }

    private void NextText()
    {
        GetText(0).text = "1번 터치시 다음 텍스트로 넘어갑니다.";
        _index++;
    }

}
