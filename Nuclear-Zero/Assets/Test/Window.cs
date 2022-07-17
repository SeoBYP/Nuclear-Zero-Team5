/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

namespace FancyScrollView.Example06
{
    enum Buttons
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5,
    }

    class Window : SubUI
    {
        [SerializeField] SlideScreenTransition transition = default;

        public void In(MovementDirection direction) => transition?.In(direction);

        public void Out(MovementDirection direction) => transition?.Out(direction);

        public override void Init()
        {
            base.Init();
            Binds();
        }

        private void Binds()
        {
            Bind<Button>(typeof(Buttons));

            BindEvent(GetButton((int)Buttons.Stage1).gameObject, OnStage1, UIEvents.Click);
            BindEvent(GetButton((int)Buttons.Stage2).gameObject, OnStage2, UIEvents.Click);
            BindEvent(GetButton((int)Buttons.Stage3).gameObject, OnStage3, UIEvents.Click);
            BindEvent(GetButton((int)Buttons.Stage4).gameObject, OnStage4, UIEvents.Click);
            BindEvent(GetButton((int)Buttons.Stage5).gameObject, OnStage5, UIEvents.Click);
        }

        private void OnStage1(PointerEventData data)
        {
            //CurrentChapter = Chapter.Chapter1;
            SceneManagerEx.Instance.LoadScene(Scene.Game);
        }

        private void OnStage2(PointerEventData data)
        {
            //CurrentChapter = Chapter.Chapter2;
            SceneManagerEx.Instance.LoadScene(Scene.Game);
        }
        private void OnStage3(PointerEventData data)
        {
            //CurrentChapter = Chapter.Chapter3;
            SceneManagerEx.Instance.LoadScene(Scene.Game);

        }
        private void OnStage4(PointerEventData data)
        {
            //CurrentChapter = Chapter.Chapter4;
            SceneManagerEx.Instance.LoadScene(Scene.Game);
        }
        private void OnStage5(PointerEventData data)
        {
            //CurrentChapter = Chapter.Chapter4;
            SceneManagerEx.Instance.LoadScene(Scene.Game);
        }
    }
}
