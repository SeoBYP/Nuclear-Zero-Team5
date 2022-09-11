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
    [SerializeField] private string url = "";
    [SerializeField] private LobbyUI lobbyUI;
    private UnityWebRequest request;
    private DateTime startTime;
    private bool onTime;

    [Obsolete]
    private void Awake()
    {
        string today = DataManager.Instance.playerInfo.Daily;
        startTime = Convert.ToDateTime(today);
        StartCoroutine(WebChk());
    }

    [Obsolete]
    public void SetToday()
    {
        StartCoroutine(SetTimeToday());
    }

    [Obsolete]
    IEnumerator SetTimeToday()
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

                DateTime dateTime = Convert.ToDateTime(date);
                string time = $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day} 09:00:00";
                DataManager.Instance.playerInfo.Daily = time;
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

                DateTime dateTime = Convert.ToDateTime(date);
                TimeSpan timedif = dateTime - startTime;
                if(timedif.Days > 0)
                {
                    if(timedif.Hours >= 0)
                    {
                        onTime = true;
                        if(DataManager.Instance.playerInfo.DailyGift)
                        {
                            DataManager.Instance.playerInfo.DailyGift = false;
                        }
                    }
                    else
                    {
                        onTime = false;
                    }
                }
                else
                {
                    if (timedif.Hours >= 0)
                    {
                        onTime = true;

                    }
                    else
                    {
                        onTime = false;
                    }
                }
            }
            lobbyUI.SetGiftBtn(onTime);
        }
    }
}
