using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy
{
    [AddComponentMenu("")]
    public class AutoGlobalScopeTracker : ScopeTracker
    {
        public override object GetOwnerObject() => UScopesInstance.GlobalScopeKey;

        public override void DestroySelf() => Destroy(gameObject);
        public override void DestroySelfImmediate() => DestroyImmediate(gameObject);
    }
}