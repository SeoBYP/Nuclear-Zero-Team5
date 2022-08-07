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
    private bool _isOnVibration = false;

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
        _isOnVibration = true;
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
        GameAudioManager.Instance.SaveSounds();
        ClosePopupUI();
    }
    private void OnVibrationHandle(PointerEventData data)
    {
        if (_isOnVibration)
        {
            _vibrationAni.Play(_vibrationOff);
            _isOnVibration = false;
            GameAudioManager.Instance.IsOnVibration(_isOnVibration);
        }
        else
        {
            _vibrationAni.Play(_vibrationOn);
            _isOnVibration = true;
            GameAudioManager.Instance.IsOnVibration(_isOnVibration);
        }
    }
}
