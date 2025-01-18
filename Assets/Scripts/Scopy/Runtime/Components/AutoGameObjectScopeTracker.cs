using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy
{
    [AddComponentMenu("")]
    public class AutoGameObjectScopeTracker : ScopeTracker
    {
        public override object GetOwnerObject() => gameObject;

        public override void DestroySelf()
        {
            Destroy(this);
        }
    }
}