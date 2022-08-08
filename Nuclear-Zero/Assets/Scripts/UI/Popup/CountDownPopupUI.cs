using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class CountDownPopupUI : PopupUI
{
    enum Texts
    {
        CountDonwTimer,
    }

    enum Images
    {
        BackGround,
    }

    private int _countdown = 3;
    private float _fade = 1;
    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        _countdown = 3;
        _fade = 1;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        GetText((int)Texts.CountDonwTimer).text = _countdown.ToString();
        GetImage((int)Images.BackGround).color = new Color(0, 0, 0, _fade);
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            _countdown--;
            _fade *= 0.5f;
            GameAudioManager.Instance.Play2DSound("CountDown");
            GetText((int)Texts.CountDonwTimer).text = _countdown.ToString();
            GetImage((int)Images.BackGround).color = new Color(0, 0, 0, _fade);
            if (_countdown < 1)
            {
                GameManager.Instance.GamePause();
                ClosePopupUI();
                break;
            }
        }
    }
}
