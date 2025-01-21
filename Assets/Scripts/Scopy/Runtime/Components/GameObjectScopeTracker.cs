using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy
{
    public class GameObjectScopeTracker : AutoGameObjectScopeTracker
    {
        [SerializeField] private ScopeInstaller[] Installers;

        protected new void Awake()
        {
            ScopyManager ??= Scopy.DefaultInstance;
            var scope = ScopyManager.GetOrCreateScope(GetOwnerObject());
            ScopyManager.RegisterTrackerComponent(scope, this);
            
            foreach (var installer in Installers)
            {
                installer.Install(scope);
            }
        }
    }
}