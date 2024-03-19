using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceB : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetSceneScope().Add(this);
    }
    
    public string GetServiceBSecret()
    {
        return "Hello";
    }

    private void OnDestroy()
    {
        gameObject.GetSceneScope().Remove(this);
    }
}
