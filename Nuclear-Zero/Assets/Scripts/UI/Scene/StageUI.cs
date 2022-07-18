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

    private void OnPlayChapter1(PointerEventData data)
    {
        CurrentChapter = Chapter.Chapter1;
        SceneManagerEx.Instance.LoadScene(Scene.Game);
    }

    private void OnPlayChapter2(PointerEventData data)
    {
        CurrentChapter = Chapter.Chapter2;
        SceneManagerEx.Instance.LoadScene(Scene.Game);
    }
    private void OnPlayChapter3(PointerEventData data)
    {
        CurrentChapter = Chapter.Chapter3;
        SceneManagerEx.Instance.LoadScene(Scene.Game);

    }
    private void OnPlayChapter4(PointerEventData data)
    {
        CurrentChapter = Chapter.Chapter4;
        SceneManagerEx.Instance.LoadScene(Scene.Game);
    }


}
