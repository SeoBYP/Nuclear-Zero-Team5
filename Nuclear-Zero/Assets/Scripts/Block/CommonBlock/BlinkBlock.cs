using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBlock : BlockController
{
    private float elapsed = 0;
    private float speed = 0.5f;
    private static Color curColor;
    private static Color curColorAlpha = new Color(0, 0, 0, 0);
    private bool isUpdate = false;
    private Color start;
    private Color end;
    private SpriteRenderer _sprite;
    private bool IsActive = true;

    protected override void Init()
    {
        base.Init();
        _sprite = GetComponent<SpriteRenderer>();
        curColor = _sprite.color;
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            yield return YieldInstructionCache.WaitForSeconds(2f);
            SetDeActiveColor();
            yield return YieldInstructionCache.WaitForSeconds(2f);
            SetActiveColor();
        }
    }

    private void SetActiveColor()
    {
        _collider2D.enabled = true;
        _trriger2D.enabled = true;
        isUpdate = true;
        start = curColorAlpha;
        end = curColor;
        elapsed = 0;
    }

    private void SetDeActiveColor()
    {
        _collider2D.enabled = false;
        _trriger2D.enabled = false;
        isUpdate = true;
        start = curColor;
        end = curColorAlpha;
        elapsed = 0;
    }

    private void Update()
    {
        if (isUpdate == false)
            return;



        elapsed += Time.deltaTime / speed;
        elapsed = Mathf.Clamp01(elapsed);
        Color color = Color.Lerp(start, end, elapsed);
        _sprite.color = color;
        if (elapsed >= 1.0f)
        {
            isUpdate = false;
        }
    }
}
