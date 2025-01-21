using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy
{
    public abstract class ScopeTracker : MonoBehaviour
    {
        public ScopyManager ScopyManager { get; set; }
        
        protected void Awake()
        {
            ScopyManager ??= Scopy.DefaultInstance;
            ScopyManager.RegisterTrackerComponent(ScopyManager.GetOrCreateScope(GetOwnerObject()), this);
        }

        public abstract object GetOwnerObject();
        public abstract void DestroySelf();
        
        private void OnDestroy()
        {
            if (ScopyManager != null)
            {
                ScopyManager.RemoveTrackerComponent(this);
            }
        }
    }
}