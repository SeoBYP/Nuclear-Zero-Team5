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

    public void SetColor(Color color)
    {
        GetImage((int)Images.ShineStar).color = color;
        GetImage((int)Images.DeShineStar).color = color;
    }

    public void SetShineStar()
    {
        GetImage((int)Images.DeShineStar).gameObject.SetActive(false);
        GetImage((int)Images.ShineStar).gameObject.SetActive(true);
    }

    public void SetDeShineStar()
    {
        GetImage((int)Images.ShineStar).gameObject.SetActive(false);
        GetImage((int)Images.DeShineStar).gameObject.SetActive(true);
    }

}
