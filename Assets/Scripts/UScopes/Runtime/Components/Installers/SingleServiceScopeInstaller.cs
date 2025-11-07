using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Okancandev.UScopes
{
    public class SingleServiceScopeInstaller : ScopeInstaller
    {
        [SerializeField] public Object[] SingleServices;
   
        public override void Install(Scope scope)
        {
            foreach (var service in SingleServices)
            {
                scope.AddSingle(service);
            }
        }
    }
}