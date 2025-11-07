using System;
using System.Collections;
using System.Collections.Generic;
using Okancandev.UScopes;
using UnityEngine;

namespace Okancandev.UScopes
{
    public class UScopesIMGUIComponent : MonoBehaviour
    {
        private readonly UScopesIMGUIDrawer _imGUIDrawer = new();

        private void OnGUI()
        {
            _imGUIDrawer.OnGUI();
        }
    }
}