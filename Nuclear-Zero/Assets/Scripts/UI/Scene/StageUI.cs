using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class StageUI : SceneUI
{
   
    public Chapter CurrentChapter { get; set; }

    public override void Init()
    {
        base.Init();
    }
}
