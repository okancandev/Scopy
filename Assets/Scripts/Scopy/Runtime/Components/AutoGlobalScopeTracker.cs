using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy
{
    [AddComponentMenu("")]
    public class AutoGlobalScopeTracker : ScopeTracker
    {
        public override object GetOwnerObject() => ScopyManager.GlobalScopeKey;

        public override void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}