using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
    private bool _awake;
    
    private void Awake()
    {
        _awake = true;
        gameObject.GetGlobalScope().Add(this);
    }
    
    public T GetService<T>()
    {
        return gameObject.GetGlobalScope().Get<T>();
    }

    private void OnDestroy()
    {
        if (_awake && !Scopy.Quiting)
            gameObject.GetGlobalScope().Remove(this);
    }
}