using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadePopupUI : PopupUI
{
    enum Images
    {
        Fade,
    }

    private float elapsed = 0;
    private float speed = 1;
    private static Color black = Color.black;
    private static Color blackAlpha = new Color(0, 0, 0, 0);
    private bool isUpdate = false;
    private Color start;
    private Color end;

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
    }
    //화면이 검은 상태에서 서서히 사라진다.
    public void FadeIn(float speed)
    {
        gameObject.SetActive(true);
        GetImage(0).color = black;
        isUpdate = true;
        this.speed = speed;
        start = black;
        end = blackAlpha;
        elapsed = 0;
    }
    //화면이 서서히 검은 화면으로 바뀐다.
    public void FadeOut(float speed)
    {
        gameObject.SetActive(true);
        GetImage(0).color = blackAlpha;
        isUpdate = true;
        this.speed = speed;
        start = blackAlpha;
        end = black;
        elapsed = 0;
    }

    void Update()
    {
        if (isUpdate == false)
            return;

        elapsed += Time.deltaTime / speed;
        elapsed = Mathf.Clamp01(elapsed);
        Color color = Color.Lerp(start, end, elapsed);
        GetImage(0).color = color;
        if (elapsed >= 1.0f)
        {
            if (color.Equals(blackAlpha))
            {
                ClosePopupUI();
            }
            isUpdate = false;
        }
    }
}
