using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.UScopes
{
    public abstract class ScopeTracker : MonoBehaviour
    {
        public UScopesInstance UScopesInstance { get; set; }
        
        protected void Awake()
        {
            UScopesInstance ??= UScopes.DefaultInstance;
            UScopesInstance.RegisterTrackerComponent(UScopesInstance.GetOrCreateScope(GetOwnerObject()), this);
        }

        public abstract object GetOwnerObject();
        public abstract void DestroySelf();
        public abstract void DestroySelfImmediate();

        internal void DetachScopyInstance()
        {
            UScopesInstance = null;
        }
        
        private void OnDestroy()
        {
            if (UScopesInstance != null)
            {
                UScopesInstance.RemoveTrackerComponent(this);
            }
        }
    }
}