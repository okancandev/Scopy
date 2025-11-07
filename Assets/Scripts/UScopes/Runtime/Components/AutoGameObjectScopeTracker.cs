using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.UScopes
{
    [AddComponentMenu("")]
    public class AutoGameObjectScopeTracker : ScopeTracker
    {
        public override object GetOwnerObject() => gameObject;

        public override void DestroySelf() => Destroy(this);
        public override void DestroySelfImmediate() => DestroyImmediate(this);
    }
}