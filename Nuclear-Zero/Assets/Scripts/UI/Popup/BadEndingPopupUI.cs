using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEndingPopupUI : PopupUI
{
    DialogueUI dialogueUI;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        dialogueUI = GetComponent<DialogueUI>();
        if (dialogueUI != null)
            dialogueUI.Init();
        GameAudioManager.Instance.PlayBackGround("BadEnding");
    }

    public override void ClosePopupUI()
    {
        GameAudioManager.Instance.PlayBackGround("LobbyBGM");
        base.ClosePopupUI();
    }
}
