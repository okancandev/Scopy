using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Service : MonoBehaviour
{
    private bool _awake;
    
    protected void Awake()
    {
        _awake = true;
        gameObject.GetGlobalScope().Add(this);
    }
    
    public T GetService<T>()
    {
        return gameObject.GetGlobalScope().Get<T>();
    }

    protected void OnDestroy()
    {
        if (_awake && !Scopy.Quiting)
            gameObject.GetGlobalScope().Remove(this);
    }
}
