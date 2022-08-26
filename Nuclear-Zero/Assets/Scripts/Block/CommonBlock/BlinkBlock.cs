using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBlock : BlockController
{
    private float elapsed = 0;
    private float speed = 1f;
    private static Color curColor;
    private static Color curColorAlpha = new Color(0, 0, 0, 0);
    private bool isUpdate = false;
    private Color start;
    private Color end;
    private SpriteRenderer _sprite;
    private bool IsActive = true;

    public float BlinkTime;
    public float StartTime;

    private bool IsStart;

    protected override void Init()
    {
        base.Init();
        _sprite = GetComponent<SpriteRenderer>();
        curColor = _sprite.color;
        IsStart = false;
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            if(IsStart == false)
            {
                yield return YieldInstructionCache.WaitForSeconds(StartTime);
                IsStart = true;
            }
                
            else
                yield return YieldInstructionCache.WaitForSeconds(BlinkTime);
            SetDeActiveColor();
            yield return YieldInstructionCache.WaitForSeconds(BlinkTime);
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
        IsActive = true;
    }

    private void SetDeActiveColor()
    {
        IsActive = false;

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
        if (elapsed >= 1f)
        {
            isUpdate = false;
            if(IsActive == false)
            {
                _collider2D.enabled = false;
                _trriger2D.enabled = false;
            }
        }
    }
}
