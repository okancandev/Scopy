using System;
using System.Collections;
using System.Collections.Generic;
using Okancandev.Scopy;
using Okancandev.Scopy.Ideas;
using UnityEngine;

public class MySceneService : SceneService, IScopyEditorCustomGUI
{
    private void Start()
    {
        Scopy.GlobalScope().AddSingle("String as service");
        Scopy.GlobalScope().Add(new ServiceIdentifier(typeof(MonoBehaviour)), this);
    }

    public void OnScopyEditorGUI()
    {
        GUILayout.Box("Ready");
        GUILayout.Box("Init");
        GUILayout.Box("Begin");
    }

    public string OnScopyEditorExtraField()
    {
        return "Inited-Started-Ready";
    }
}
