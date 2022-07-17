using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile<T> : MonoBehaviour where T : Component
{
    public virtual void Init()
    {

    }

    public virtual void Excute()
    {

    }

    protected void Run()
    {
        Excute();
    }

    private void Update()
    {
        Run();
    }
}
