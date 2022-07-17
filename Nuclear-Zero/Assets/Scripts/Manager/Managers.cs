using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers<T> : MonoBehaviour where T : Managers<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Utils.CreateObject<T>(null);
                if(instance != null)
                {
                    instance.Init();
                    instance.hideFlags = HideFlags.HideAndDontSave;
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    public virtual void Init()
    {

    }
    public virtual void Release()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }
}
