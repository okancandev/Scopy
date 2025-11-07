using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy
{
    [AddComponentMenu("")]
    public class AutoSceneScopeTracker : ScopeTracker
    {
        public override object GetOwnerObject() => gameObject.scene;

        public override void DestroySelf() => Destroy(gameObject);
        public override void DestroySelfImmediate() => DestroyImmediate(gameObject);
    }
}

