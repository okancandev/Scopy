using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceA : MonoBehaviour
{
    private ServiceB _serviceB;
    
    private void Awake()
    {
        gameObject.GetSceneScope().Add(this);
    }

    private void Start()
    {
        _serviceB = gameObject.GetSceneScope().Get<ServiceB>();
        Debug.Log(_serviceB.GetServiceBSecret());
    }

    private void OnDestroy()
    {
        gameObject.GetSceneScope().Remove(this);
    }
}