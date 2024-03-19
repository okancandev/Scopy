using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceA : SceneService
{
    private ServiceB _serviceB;
    
    private void Start()
    {
        _serviceB = GetService<ServiceB>();
        Debug.Log(_serviceB.GetServiceBSecret());
    }
}