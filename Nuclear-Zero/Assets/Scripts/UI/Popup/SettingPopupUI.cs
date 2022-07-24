using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class SettingPopupUI : PopupUI
{
    enum GameObjects
    {
        Panel,
    }
    enum Sliders
    {
        BGMProgressBar,
        EffectProgressBar,
    }
    enum Buttons
    {
        VibrationHandle,
        Close,
    }
    private bool IsOn = false;

    private Slider _bgmSlider;
    private Slider _effectSlider;
    private Animation _vibrationAni;
    private string _vibrationOn = "VibrationOn";
    private string _vibrationOff = "VibrationOff";

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        IsOn = false;
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.VibrationHandle).gameObject, OnVibrationHandle, UIEvents.Click);
        SetSliders();
        GetAnimations();
    }

    private void SetSliders()
    {
        _bgmSlider = Get<Slider>((int)Sliders.BGMProgressBar);
        _bgmSlider.onValueChanged.AddListener(OnBGMSliderChanged);
        _bgmSlider.value = GameAudioManager.Instance.BGMSound;

        _effectSlider = Get<Slider>((int)Sliders.EffectProgressBar);
        _effectSlider.onValueChanged.AddListener(OnEffectSliderChanged);
        _effectSlider.value = GameAudioManager.Instance.EffectSound;
    }

    private void GetAnimations()
    {
        GameObject go = GetButton((int)Buttons.VibrationHandle).gameObject;
        _vibrationAni = go.GetComponent<Animation>();
    }

    private void OnBGMSliderChanged(float value)
    {
        if(value <= 0.14f)
        {
            _bgmSlider.value = 0.14f;
        }
        GameAudioManager.Instance.SetBGMSound(_bgmSlider.value);
    }

    private void OnEffectSliderChanged(float value)
    {
        if (value <= 0.14f)
        {
            _effectSlider.value = 0.14f;
        }
        GameAudioManager.Instance.SetEffectSound(_effectSlider.value);
    }
        private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }
    private void OnVibrationHandle(PointerEventData data)
    {
        if (IsOn)
        {
            _vibrationAni.Play(_vibrationOff);
            IsOn = false;
        }
        else
        {
            _vibrationAni.Play(_vibrationOn);
            IsOn = true;
        }
    }
}
