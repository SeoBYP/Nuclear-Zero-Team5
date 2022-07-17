using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultStar : SubUI
{
    Image ShineStar;
    Image DeShineStar;
    public override void Init()
    {
        base.Init();

        ShineStar = GetComponentInChildren<Image>();
        DeShineStar = GetComponentInChildren<Image>();
    }

    public void SetShineStar()
    {
        ShineStar.gameObject.SetActive(true);
        DeShineStar.gameObject.SetActive(false);
    }

    public void SetDeShineStar()
    {
        ShineStar.gameObject.SetActive(false);
        DeShineStar.gameObject.SetActive(true);
    }
}
