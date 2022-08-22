using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
using System;
public class TimeProject : SubUI
{
    enum Texts
    {
        TimeCount,
    }

    enum Buttons
    {
        ADButton,
    }

    enum GameObjects
    {
        NonRecItem,
        RecItem,
    }

    [SerializeField] private string url = "";
    private UnityWebRequest request;
    private DateTime startTime;
    private bool _isUpdate;
    
    [Obsolete]
    private void Start()
    {
        StartCoroutine(WebChk());   
    }

    public override void Init()
    {
        base.Init();
        Binds();
        GoogleMobileAdsManager.Instance.RequestAndLoadRewardedInterstitialAd();
    }

    private void Binds()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        _isUpdate = true;
    }

    private void SetNonRecItem()
    {
        GetGameObject((int)GameObjects.NonRecItem).gameObject.SetActive(true);
        GetGameObject((int)GameObjects.RecItem).gameObject.SetActive(false);
        BindEvent(GetButton((int)Buttons.ADButton).gameObject, OnRequestAd, UIEvents.Click);
    }

    private void SetRecItem(TimeSpan time)
    {
        GetGameObject((int)GameObjects.RecItem).gameObject.SetActive(true);
        GetGameObject((int)GameObjects.NonRecItem).gameObject.SetActive(false);
        string text = $"{time.Hours}:{time.Minutes}:{time.Seconds}";
        GetText((int)Texts.TimeCount).text = text;
    }

    private void OnRequestAd(PointerEventData data)
    {
        GoogleMobileAdsManager.Instance.ShowRewardedInterstitialAd();
    }

    private void Update()
    {
        if (_isUpdate)
        {
            if(request != null)
            {
                //if (request.isNetworkError)
                //{
                //    Debug.Log(request.error);
                //}
                //else
                //{
                //    string date = request.GetResponseHeader("date");
                //    DateTime dateTime = Convert.ToDateTime(date);
                //    if (startTime == null)
                //    {
                //        startTime = dateTime;
                //        SetNonRecItem();
                //    }
                //    else
                //    {
                //        TimeSpan timeDif = dateTime - startTime;
                //        SetRecItem(timeDif);
                //    }
                //}
            }
        }
    }

    [Obsolete]
    IEnumerator WebChk()
    {
        request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date");

                DateTime dateTime = Convert.ToDateTime(date);//DateTime.Parse(date).ToUniversalTime();
                if (startTime == null)
                {
                    startTime = dateTime;
                    SetNonRecItem();
                }
                else
                {
                    TimeSpan timeDif = dateTime - startTime;
                    SetRecItem(timeDif);
                }
            }
        }
    }
}
