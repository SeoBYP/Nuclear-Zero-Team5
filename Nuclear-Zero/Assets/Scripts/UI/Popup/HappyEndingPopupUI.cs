using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyEndingPopupUI : PopupUI
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
    }

    public override void ClosePopupUI()
    {
        //UIManager.Instance.FadeIn();
        base.ClosePopupUI();
    }
}
