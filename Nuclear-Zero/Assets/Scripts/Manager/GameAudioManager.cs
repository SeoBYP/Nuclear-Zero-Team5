using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GameAudioManager : Managers<GameAudioManager>, IUpdate
{
    private AudioSource _backGround;
    private List<AudioSource> effectAudioList = new List<AudioSource>();
    private Dictionary<string, AudioClip> clipDic = new Dictionary<string, AudioClip>();

    private int _limitCount = 10;
    private float _intervalTime = 2;
    private float prevTime = 0;

    public float BGMSound { get; private set; }
    public float EffectSound { get; private set; }
    private bool _IsVibration = true;

    public override void Init()
    {
        BGMSound = DataManager.Instance.playerInfo.BGMSound;
        EffectSound = DataManager.Instance.playerInfo.BGMSound;
        _backGround = gameObject.GetOrAddComponent<AudioSource>();
        _backGround.spatialBlend = 0;
        _backGround.volume = 1.0f;
        _backGround.playOnAwake = false;
        _IsVibration = true;
        LoadSound();

        UpdateManager.Instance.Listener(this);
    }

    public void PlayBackGround(string name)
    {
        if (clipDic.ContainsKey(name))
        {
            _backGround.clip = clipDic[name];
            _backGround.volume = BGMSound;
            _backGround.loop = true;
            _backGround.Play();
        }
    }

    AudioSource Pooling()
    {
        AudioSource audioSource = null;
        for (int i = 0; i < effectAudioList.Count; i++)
        {
            if (effectAudioList[i].gameObject.activeSelf == false)
            {
                audioSource = effectAudioList[i];
                audioSource.gameObject.SetActive(true);
                break;
            }
        }
        if (audioSource == null)
        {
            audioSource = Utils.CreateObject<AudioSource>(transform);
            effectAudioList.Add(audioSource);
        }
        return audioSource;
    }

    IEnumerator IDeactiveAudio(AudioSource audio)
    {
        yield return new WaitForSeconds(audio.clip.length);
        audio.gameObject.SetActive(false);
    }

    private void Play(string name, float spatialBlend, Vector3 position)
    {
        if (name == null || clipDic.ContainsKey(name) == false)
        {
            Debug.Log("No Sound In clipDic : " + name);
            return;
        }
        AudioSource audioSource = Pooling();
        audioSource.clip = clipDic[name];
        audioSource.spatialBlend = spatialBlend;
        audioSource.volume = EffectSound;
        audioSource.Play();
        StartCoroutine(IDeactiveAudio(audioSource));
    }

    public void Play2DSound(string name)
    {
        Play(name, 0, Vector3.zero);
    }

    
    #region Play2DSoundLoop
    private float _loopSound;
    private AudioSource _backEnemyWarning;
    private void PlayLoop(string name, float spatialBlend, Vector3 position, bool loop = false)
    {
        if (name == null || clipDic.ContainsKey(name) == false)
        {
            Debug.Log("No Sound In clipDic : " + name);
            return;
        }
        _backEnemyWarning = gameObject.AddComponent<AudioSource>();
        _backEnemyWarning.clip = clipDic[name];
        _backEnemyWarning.spatialBlend = spatialBlend;
        _backEnemyWarning.volume = _loopSound;
        _backEnemyWarning.loop = loop;
        _backEnemyWarning.Play();
        //StartCoroutine(IDeactiveAudio(audioSource));
    }

    public void Play2DSoundLoop(string name)
    {
        PlayLoop(name, 0, Vector3.zero,true);
    }

    public void Set2DLoopSound(float volum)
    {
        _loopSound = volum;
        if (_backEnemyWarning != null)
            _backEnemyWarning.volume = _loopSound;
    }

    public void DestroyWarning()
    {
        _backEnemyWarning = null;
    }

    #endregion

    public void LoadSound()
    {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Sound");
        for (int i = 0; i < audioClips.Length; i++)
        {
            if (clipDic.ContainsKey(audioClips[i].name) == false)
            {
                clipDic.Add(audioClips[i].name, audioClips[i]);
            }
        }
    }
    public void SetBGMSound(float value)
    {
        if (value == 0.14f)
            value = 0;
        BGMSound = value;
        if (_backGround != null)
            _backGround.volume = BGMSound;
    }
    public void SetEffectSound(float value)
    {
        if (value == 0.14f)
            value = 0;
        EffectSound = value;
    }
    public void SaveSounds() {DataManager.Instance.playerInfo.SetSoundSetting(BGMSound, EffectSound); }

    public void IsOnVibration(bool state) { _IsVibration = state; }

    public void SetVibration()
    {
        if (_IsVibration)
        {
            print("SetVibration");
            Handheld.Vibrate();
        }
    }

    public void OnUpdate()
    {
        if (effectAudioList.Count > _limitCount)
        {
            float elapsedTime = Time.time - prevTime;
            if (elapsedTime > _intervalTime)
            {
                for (int i = 0; i < effectAudioList.Count; i++)
                {
                    if (effectAudioList[i].gameObject.activeSelf == false)
                    {
                        AudioSource audioSource = effectAudioList[i];
                        effectAudioList.RemoveAt(i);
                        prevTime = Time.time;
                        Destroy(audioSource.gameObject);
                        return;
                    }
                }
            }
        }
    }
}
