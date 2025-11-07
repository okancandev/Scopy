using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Okancandev.UScopes
{
    public struct HierarchicScope
    {
        private UScopesInstance UScopesInstance { get; set; }
        private object Owner { get; set; }
        
        public HierarchicScope(object owner, UScopesInstance uScopesInstance = null)
        {
            UScopesInstance = uScopesInstance ?? UScopes.DefaultInstance;
            Owner = owner;
        }

        private Scope GetScope()
        {
            if (Owner is GameObject gameObject)
            {
                if (UScopesInstance.TryGetScope(gameObject, out Scope gameObjectScope))
                {
                    return gameObjectScope;
                }
                
                if (UScopesInstance.TryGetScope(gameObject.scene, out Scope sceneScope))
                {
                    return sceneScope;
                }
                
                if (UScopesInstance.TryGetScope(UScopesInstance.GlobalScopeKey, out Scope globalScope))
                {
                    return globalScope;
                }
            }

            if (Owner is Scene scene)
            {
                if (UScopesInstance.TryGetScope(scene, out Scope sceneScope))
                {
                    return sceneScope;
                }
                
                if (UScopesInstance.TryGetScope(UScopesInstance.GlobalScopeKey, out Scope globalScope))
                {
                    return globalScope;
                }
            }
            
            if (Owner == UScopesInstance.GlobalScopeKey)
            {
                if (UScopesInstance.TryGetScope(UScopesInstance.GlobalScopeKey, out Scope globalScope))
                {
                    return globalScope;
                }
            }
            
            return null;
        }

        private object NextOwner()
        {
            if (Owner is GameObject gameObject)
            {
                return gameObject.scene;
            }

            if (Owner != UScopesInstance.GlobalScopeKey)
            {
                return UScopesInstance.GlobalScopeKey;
            }

            return null;
        }

        public object Get(ServiceIdentifier identifier)
        {
            while (true)
            {
                var scope = GetScope();
                if (scope == null)
                {
                    throw new KeyNotFoundException($"The given key '{identifier}' was not present in the dictionary.");
                }
                
                if (scope.TryGet(identifier, out object service))
                {
                    return service;
                }

                Owner = NextOwner();
            }
        }

        public bool TryGet(ServiceIdentifier identifier, out object service)
        {
            while (true)
            {
                var scope = GetScope();
                if (scope == null)
                {
                    service = default;
                    return false;
                }
                
                if (scope.TryGet(identifier, out service))
                {
                    return true;
                }

                Owner = NextOwner();
            }
        }

        public bool TryGet<T>(ServiceIdentifier identifier, out T service) 
        {
            bool result = TryGet(identifier, out object value);
            service = (T)value;
            return result;
        }
        
        public object GetOrDefault(ServiceIdentifier identifier, object defaultValue = default) 
        {
            return TryGet(identifier, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetSingle(Type type)
        {
            return Get(new ServiceIdentifier(type));
        }
        
        public object GetTagged(Type type, object tag)
        {
            return Get(new ServiceIdentifier(type, tag));
        }
        
        public object GetWithId(Type type, long id)
        {
            return Get(new ServiceIdentifier(type, id));
        }
        
        public object GetTaggedWithId(Type type, object tag, long id)
        {
            return Get(new ServiceIdentifier(type, tag, id));
        }
        
        public T GetSingle<T>()
        {
            return (T)Get(new ServiceIdentifier(typeof(T)));
        }
        
        public T GetTagged<T>(object tag)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), tag));
        }
        
        public T GetWithId<T>(long id)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), id));
        }
        
        public T GetTaggedWithId<T>(object tag, long id)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), tag, id));
        }
        
        public bool TryGetSingle(Type serviceType, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType), out service);
        }
        
        public bool TryGetTagged(Type serviceType, object tag, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, tag), out service);
        }
        
        public bool TryGetWithId(Type serviceType, long id, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, id), out service);
        }
        
        public bool TryGetTaggedWithId(Type serviceType, object tag, long id, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, tag, id), out service);
        }

        public bool TryGetSingle<T>(out T service)
        {
            return TryGet(new ServiceIdentifier(typeof(T)), out service);
        }
        
        public bool TryGetTagged<T>(object tag, out T service)
        {
            return TryGet(new ServiceIdentifier(typeof(T), tag), out service);
        }
        
        public bool TryGetWithId<T>(long id, out T service) 
        {
            return TryGet(new ServiceIdentifier(typeof(T), id), out service);
        }
        
        public bool TryGetTaggedWithId<T>(object tag, long id, out T service) 
        {
            return TryGet(new ServiceIdentifier(typeof(T), tag, id), out service);
        }
        
        public object GetSingleOrDefault(Type type, object defaultValue = default)
        {
            return TryGetSingle(type, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedOrDefault(Type type, object tag, object defaultValue = default)
        {
            return TryGetTagged(type, tag, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetWithIdOrDefault(Type type, long id, object defaultValue = default)
        {
            return TryGetWithId(type, id, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedWithIdOrDefault(Type type, object tag, long id , object defaultValue = default)
        {
            return TryGetTaggedWithId(type, tag, id, out object value) 
                ? value 
                : defaultValue;
        }
        
        public T GetSingleOrDefault<T>(T defaultValue = default)
        {
            return TryGetSingle(out T value) 
                ? value 
                : defaultValue;
        }
        
        public T GetTaggedOrDefault<T>(object tag, T defaultValue = default)
        {
            return TryGetTagged(tag, out T value) 
                ? value 
                : defaultValue;
        }
        
        public T GetWithIdOrDefault<T>(long id, T defaultValue = default)
        {
            return TryGetWithId(id, out T value) 
                ? value 
                : defaultValue;
        }
        
        public T GetTaggedWithIdOrDefault<T>(object tag, long id, T defaultValue = default)
        {
            return TryGetTaggedWithId(tag, id, out T value) 
                ? value 
                : defaultValue;
        }
    }
}

