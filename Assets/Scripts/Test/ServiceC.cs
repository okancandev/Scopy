using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceC : IDisposable
{
    public ServiceC()
    {
        Scopy.GetGlobalScope().Add(this);
    }
    
    public void Dispose()
    {
        Scopy.GetGlobalScope().Remove(this);
    }
}
