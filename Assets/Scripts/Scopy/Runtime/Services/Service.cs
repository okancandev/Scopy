using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scopy.User
{
    public abstract class Service : MonoBehaviour
    {
        private bool _awake;
        
        public bool IsAwake() => _awake;
    
        protected void Awake()
        {
            _awake = true;
            gameObject.GetGlobalScope().Add(this);
            OnAwake();
        }
        
        protected virtual void OnAwake()
        {
            
        }
    
        public T GetService<T>()
        {
            return gameObject.GetGlobalScope().Get<T>();
        }

        protected void OnDestroy()
        {
            if (_awake && !Scopy.Quiting)
            {
                gameObject.GetGlobalScope().Remove(this);
                OnSafeDestroy();       
            }
        }
        
        protected virtual void OnSafeDestroy()
        {
            
        }
    }
}