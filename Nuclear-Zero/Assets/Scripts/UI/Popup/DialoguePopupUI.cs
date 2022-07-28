using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePopupUI : PopupUI
{
    DialogueUI dialogueUI;

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
