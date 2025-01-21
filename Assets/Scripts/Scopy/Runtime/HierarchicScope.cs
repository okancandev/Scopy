using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Okancandev.Scopy
{
    public class HierarchicScope
    {
        public ScopyManager ScopyManager { get; private set; }
        public Scope Scope { get; private set; }
        public object Owner { get; private set; }
        public HierarchicScope ParentScope { get; set; }
        
        public HierarchicScope(Scope scope, HierarchicScope parentScope = null, ScopyManager scopyManager = null)
        {
            ScopyManager = scopyManager ?? Scopy.DefaultInstance;
            Scope = scope;
            Owner = ScopyManager.FindOwner(scope);
            ParentScope = parentScope ?? FindParentScope();
        }

        private HierarchicScope FindParentScope()
        {
            if (Owner == ScopyManager.GlobalScope())
            {
                return null;
            }
            
            if (Owner is GameObject gameObjectOwner)
            {
                return gameObjectOwner.SceneScope().AsHierarchic(ScopyManager);
            }

            return ScopyManager.GlobalScope().AsHierarchic(ScopyManager);
        }
        
        public object Get(ServiceIdentifier identifier)
        {
            var result = Scope.GetOrDefault(identifier);
            if (result == default)
            {
                if (ParentScope == null)
                {
                    //this always throws by design
                    return Scope.Get(identifier); 
                }
                return ParentScope.GetOrDefault(identifier);
            }
            return result;
        }
        
        public bool TryGet(ServiceIdentifier identifier, out object service)
        {
            if (Scope.TryGet(identifier, out service))
            {
                return true;
            }

            if (ParentScope != null)
            {
                return ParentScope.TryGet(identifier, out service);
            }
            
            return false;
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
        
        public object GetSingle<T>()
        {
            return Get(new ServiceIdentifier(typeof(T)));
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

