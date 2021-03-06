using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    public bool IsUsing;
    [SerializeField] private float timer = 10;

    public void Init()
    {
        IsUsing = true;
        this.gameObject.SetActive(true);
        StartCoroutine(Deactive());
    }

    IEnumerator Deactive()
    {
        yield return YieldInstructionCache.WaitForSeconds(timer);
        IsUsing = false;
        gameObject.GetComponent<Arrow>().Clear();
        gameObject.SetActive(false);
    }
}
