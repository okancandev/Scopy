using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceA : GameObjectService
{
    private ServiceB _serviceB;
    
    private void Start()
    {
        var x = new ServiceC();
        _serviceB = GetService<ServiceB>();
        Debug.Log(_serviceB.GetServiceBSecret());
    }
}