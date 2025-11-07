using System;
using System.Collections;
using System.Collections.Generic;
using Okancandev.Scopy;
using UnityEngine;

namespace Okancandev.Scopy
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