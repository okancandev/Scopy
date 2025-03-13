using System;
using Okancandev.Scopy;
using UnityEngine;

namespace Okancandev.Scopy.Ideas
{
    public abstract class Service : MonoBehaviour
    {
        protected virtual void Awake()
        {
            RegisterService();
        }
    
        protected virtual Scope GetScope(bool createIfNotExist = true)
        {
            return gameObject.GlobalScope(createIfNotExist);
        }

        protected virtual ServiceIdentifier GetServiceIdentifier()
        {
            return new ServiceIdentifier(GetType());
        }

        protected virtual void RegisterService()
        {
            GetScope().Add(GetServiceIdentifier(), this);
        }

        protected virtual T GetSingle<T>()
        {
            return gameObject.HierarchicScope().GetSingle<T>(); 
        }
    
        protected virtual void UnRegisterService()
        {
            GetScope(false)?.Remove(GetServiceIdentifier());
        }

        protected virtual void OnDestroy()
        {
            UnRegisterService();
        }
    }
}