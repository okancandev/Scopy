using System;
using System.Collections;
using System.Collections.Generic;
using Okancandev.UScopes;
using UnityEngine;

public class Test : MonoBehaviour, IUScopesEditorCustomGUI
{
    public override string ToString()
    {
        return "Inited-Started-Ready";
    }

    public void OnUScopesEditorGUI()
    {
        GUILayout.Box("Ready");
        GUILayout.Box("Init");
        GUILayout.Box("Begin");
    }
}
