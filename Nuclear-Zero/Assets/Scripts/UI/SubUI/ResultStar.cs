using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultStar : SubUI
{
    enum Images
    {
        ShineStar,
        DeShineStar,
    }
    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
    }

    public void SetShineStar()
    {
        GetImage((int)Images.ShineStar).gameObject.SetActive(true);
        GetImage((int)Images.DeShineStar).gameObject.SetActive(false);
    }

    public void SetDeShineStar()
    {
        GetImage((int)Images.ShineStar).gameObject.SetActive(false);
        GetImage((int)Images.DeShineStar).gameObject.SetActive(true);
    }
}
