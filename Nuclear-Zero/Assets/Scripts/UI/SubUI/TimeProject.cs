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
    private DateTime startTime = Convert.ToDateTime("2022-08-26 09:00:00");
    private bool onTime;

    [Obsolete]
    private void Awake()
    {
        StartCoroutine(WebChk());
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
