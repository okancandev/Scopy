using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Okancandev.Scopy
{
    [DefaultExecutionOrder(-10000)]
    public class GlobalScopeTracker : AutoGlobalScopeTracker
    {
        [SerializeField] private new bool DontDestroyOnLoad;
        [SerializeField] private ScopeInstaller[] Installers;

        protected new void Awake()
        {
            ScopyInstance ??= Scopy.DefaultInstance;
            var scope = ScopyInstance.GetOrCreateScope(GetOwnerObject());
            ScopyInstance.RegisterTrackerComponent(scope, this);
            
            if (DontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            
            foreach (var installer in Installers)
            {
                installer.Install(scope);
            }
        }
    }
}