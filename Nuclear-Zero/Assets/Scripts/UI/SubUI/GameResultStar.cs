using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultStar : SubUI
{
    enum Images
    {
        ShineStar,
        DeShineStar,
    }

    [SerializeField] private ParticleSystem _effect;

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        _effect.gameObject.SetActive(false);
    }

    public void Deactive()
    {
        GetImage((int)Images.ShineStar).gameObject.SetActive(false);
        GetImage((int)Images.DeShineStar).gameObject.SetActive(false);
    }

    public void SetShineStar()
    {
        _effect.gameObject.SetActive(true);
        GetImage((int)Images.ShineStar).gameObject.SetActive(true);
    }

    public void SetDeShineStar()
    {
        GetImage((int)Images.DeShineStar).gameObject.SetActive(true);
    }

}
