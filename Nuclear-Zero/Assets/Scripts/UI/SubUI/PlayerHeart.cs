using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : SubUI
{
    enum Images
    {
        LifeHeart,
        GrayHeart,
    }
    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        SetLifeHeart();
    }

    public void SetLifeHeart()
    {
        GetImage((int)Images.LifeHeart).gameObject.SetActive(true);
        GetImage((int)Images.GrayHeart).gameObject.SetActive(false);
    }

    public void SetGrayHeart()
    {
        GetImage((int)Images.LifeHeart).gameObject.SetActive(false);
        GetImage((int)Images.GrayHeart).gameObject.SetActive(true);
    }
}
