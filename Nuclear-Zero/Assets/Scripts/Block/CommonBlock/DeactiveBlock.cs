using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveBlock : BlockController
{
    private float elapsed = 0;
    private float speed = 1;
    private static Color curColor;
    private static Color curColorAlpha = new Color(0, 0, 0, 0);
    private bool isUpdate = false;
    private Color start;
    private Color end;
    private SpriteRenderer _sprite;
    protected override void Init()
    {
        base.Init();
        isUpdate = false;
        _sprite = GetComponent<SpriteRenderer>();
        curColor = _sprite.color;
    }
    public override void OnSteped()
    {
        base.OnSteped();
        isUpdate = true;
        start = curColor;
        end = curColorAlpha;
        elapsed = 0;
        //StartCoroutine(ResetColor());
    }

    //IEnumerator ResetColor()
    //{
    //    yield return YieldInstructionCache.WaitForSeconds(3f);
    //    _collider2D.enabled = true;
    //    _trriger2D.enabled = true;
    //    _sprite.color = curColor;
    //}

    void Update()
    {
        if (isUpdate)
        {
            elapsed += Time.deltaTime / speed;
            elapsed = Mathf.Clamp01(elapsed);
            Color color = Color.Lerp(start, end, elapsed);
            _sprite.color = color;
            if (elapsed >= 1.0f)
            {
                if (color.Equals(curColorAlpha))
                {
                    //StartCoroutine(ResetColor());
                    _collider2D.enabled = false;
                    _trriger2D.enabled = false;
                }
                isUpdate = false;
            }
        }
    }
}
