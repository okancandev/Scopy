using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Okancandev.Scopy
{
    public struct HierarchicScope
    {
        private ScopyManager ScopyManager { get; set; }
        private object Owner { get; set; }
        
        public HierarchicScope(object owner, ScopyManager scopyManager = null)
        {
            ScopyManager = scopyManager ?? Scopy.DefaultInstance;
            Owner = owner;
        }

        private Scope GetScope()
        {
            if (Owner is GameObject gameObject)
            {
                if (ScopyManager.TryGetScope(gameObject, out Scope gameObjectScope))
                {
                    return gameObjectScope;
                }
                
                if (ScopyManager.TryGetScope(gameObject.scene, out Scope sceneScope))
                {
                    return sceneScope;
                }
                
                if (ScopyManager.TryGetScope(ScopyManager.GlobalScopeKey, out Scope globalScope))
                {
                    return globalScope;
                }
            }

            if (Owner is Scene scene)
            {
                if (ScopyManager.TryGetScope(scene, out Scope sceneScope))
                {
                    return sceneScope;
                }
                
                if (ScopyManager.TryGetScope(ScopyManager.GlobalScopeKey, out Scope globalScope))
                {
                    return globalScope;
                }
            }
            
            if (Owner == ScopyManager.GlobalScopeKey)
            {
                if (ScopyManager.TryGetScope(ScopyManager.GlobalScopeKey, out Scope globalScope))
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

            if (Owner != ScopyManager.GlobalScopeKey)
            {
                return ScopyManager.GlobalScopeKey;
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
        
        public object GetWithId(Type type ,long id)
        {
            return Get(new ServiceIdentifier(type, id));
        }
        
        public object GetTagged(Type type, object tag)
        {
            return Get(new ServiceIdentifier(type, tag));
        }
        
        public object GetTaggedWithId(Type type, long id, object tag)
        {
            return Get(new ServiceIdentifier(type, id, tag));
        }
        
        public T GetSingle<T>()
        {
            return (T)Get(new ServiceIdentifier(typeof(T)));
        }
        
        public T GetWithId<T>(long id)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), id));
        }
        
        public T GetTagged<T>(object tag)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), tag));
        }
        
        public T GetTaggedWithId<T>(long id, object tag)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), id, tag));
        }
        
        public bool TryGetSingle(Type serviceType, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType), out service);
        }
        
        public bool TryGetWithId(Type serviceType, long id, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, id), out service);
        }
        
        public bool TryGetTagged(Type serviceType, object tag, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, tag), out service);
        }
        
        public bool TryGetTaggedWithId(Type serviceType, long id, object tag, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, id, tag), out service);
        }

        public bool TryGetSingle<T>(out T service)
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T)), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetWithId<T>(long id, out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), id), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetTagged<T>(object tag, out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), tag), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetTaggedWithId<T>(long id, object tag, out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), id, tag), out var value);
            service = (T)value;
            return result;
        }
        
        public object GetSingleOrDefault(Type type, object defaultValue = default)
        {
            return TryGetSingle(type, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetWithIdOrDefault(Type type, long id, object defaultValue = default)
        {
            return TryGetWithId(type, id, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedOrDefault(Type type, object tag, object defaultValue = default)
        {
            return TryGetTagged(type, tag, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedWithIdOrDefault(Type type, long id, object tag, object defaultValue = default)
        {
            return TryGetTaggedWithId(type, id, tag, out object value) 
                ? value 
                : defaultValue;
        }
        
        public T GetSingleOrDefault<T>(object defaultValue = default)
        {
            return TryGetSingle(typeof(T), out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetWithIdOrDefault<T>(long id, object defaultValue = default)
        {
            return TryGetWithId(typeof(T), id, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetTaggedOrDefault<T>(object tag, object defaultValue = default)
        {
            return TryGetTagged(typeof(T), tag, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetTaggedWithIdOrDefault<T>(long id, object tag, object defaultValue = default)
        {
            return TryGetTaggedWithId(typeof(T), id, tag, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
    }
}

