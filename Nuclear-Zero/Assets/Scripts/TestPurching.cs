using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class TestPurching : MonoBehaviour
{
    private void Start()
    {
        StandardPurchasingModule.Instance().useFakeStoreAlways = true;
    }

    public void Reward()
    {
        //StandardPurchasingModule.Instance().useFakeStoreUIMode
        Debug.Log("인앱결제 성공");
        
    }

    public void Failed()
    {
        Debug.Log("인앱결제 실패");
    }
}
