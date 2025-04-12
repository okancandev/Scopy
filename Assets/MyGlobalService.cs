using System;
using System.Collections;
using System.Collections.Generic;
using Okancandev.Scopy;
using Okancandev.Scopy.Ideas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGlobalService : Service
{
    private void Start()
    {
        Scopy.GameObjectScope(gameObject).AddSingle(this);
    }
}
