using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy
{
    public abstract class ScopeTracker : MonoBehaviour
    {
        public ScopyInstance ScopyInstance { get; set; }
        
        protected void Awake()
        {
            ScopyInstance ??= Scopy.DefaultInstance;
            ScopyInstance.RegisterTrackerComponent(ScopyInstance.GetOrCreateScope(GetOwnerObject()), this);
        }

        public abstract object GetOwnerObject();
        public abstract void DestroySelf();
        
        private void OnDestroy()
        {
            if (ScopyInstance != null)
            {
                ScopyInstance.RemoveTrackerComponent(this);
            }
        }
    }
}