using System;
using System.Collections;
using System.Collections.Generic;
using Okancandev.Scopy;
using UnityEngine;

public class Test : MonoBehaviour, IScopyEditorCustomGUI
{
    public override string ToString()
    {
        return "Inited-Started-Ready";
    }

    public void OnScopyEditorGUI()
    {
        GUILayout.Box("Ready");
        GUILayout.Box("Init");
        GUILayout.Box("Begin");
    }
}
