using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.UScopes
{
    [DefaultExecutionOrder(-8000)]
    public class GameObjectScopeTracker : AutoGameObjectScopeTracker
    {
        [SerializeField] private ScopeInstaller[] Installers;

        protected new void Awake()
        {
            UScopesInstance ??= UScopes.DefaultInstance;
            var scope = UScopesInstance.GetOrCreateScope(GetOwnerObject());
            UScopesInstance.RegisterTrackerComponent(scope, this);
            
            foreach (var installer in Installers)
            {
                installer.Install(scope);
            }
        }
    }
}