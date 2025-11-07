using System;
using UnityEngine;

namespace Okancandev.UScopes.Recipes
{
    public abstract class ScopedMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            RegisterService();
        }

        protected abstract Scope GetScope(bool createIfNotExist = true);

        protected virtual ServiceIdentifier GetServiceIdentifier()
        {
            return new ServiceIdentifier(GetType());
        }

        protected virtual void RegisterService()
        {
            GetScope().Add(GetServiceIdentifier(), this);
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