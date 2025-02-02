using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy
{
    [DefaultExecutionOrder(-8000)]
    public class GameObjectScopeTracker : AutoGameObjectScopeTracker
    {
        [SerializeField] private ScopeInstaller[] Installers;

        protected new void Awake()
        {
            ScopyInstance ??= Scopy.DefaultInstance;
            var scope = ScopyInstance.GetOrCreateScope(GetOwnerObject());
            ScopyInstance.RegisterTrackerComponent(scope, this);
            
            foreach (var installer in Installers)
            {
                installer.Install(scope);
            }
        }
    }
}