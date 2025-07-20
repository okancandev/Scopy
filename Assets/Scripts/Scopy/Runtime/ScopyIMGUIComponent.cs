using System;
using System.Collections;
using System.Collections.Generic;
using Okancandev.Scopy;
using UnityEngine;

namespace Okancandev.Scopy
{
    public class ScopyIMGUIComponent : MonoBehaviour
    {
        private readonly ScopyIMGUIDrawer _imGUIDrawer = new();

        private void OnGUI()
        {
            _imGUIDrawer.OnGUI();
        }
    }
}