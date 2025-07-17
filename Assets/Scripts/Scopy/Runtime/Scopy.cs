using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[assembly:InternalsVisibleTo("Scopy.Editor")]
namespace Okancandev.Scopy
{
    public static class Scopy
    {
        internal static ScopyInstance _defaultInstance;

        public static ScopyInstance DefaultInstance
        {
            get => _defaultInstance ??= new ScopyInstance();
            private set => _defaultInstance = value;
        }

        public static Scope GlobalScope(bool createIfNotExist = true)
        {
            return DefaultInstance.GlobalScope(createIfNotExist);
        }
        
        public static Scope SceneScope(Scene scene, bool createIfNotExist = true)
        {
            return DefaultInstance.SceneScope(scene, createIfNotExist);
        }
        
        public static Scope ActiveSceneScope(bool createIfNotExist = true)
        {
            return DefaultInstance.ActiveSceneScope(createIfNotExist);
        }
        
        public static Scope GameObjectScope(GameObject gameObject, bool createIfNotExist = true)
        {
            return DefaultInstance.GameObjectScope(gameObject, createIfNotExist);
        }
        
        public static Scope CustomScope(object owner, bool createIfNotExist = true)
        {
#if UNITY_EDITOR
            ScopeKeyValidation(owner);
#endif
            if (DefaultInstance.TryGetScope(owner, out var scope))
            {
                return scope;
            }

            if (createIfNotExist)
            {
                return DefaultInstance.CreateScope(owner);
            }
                
            return null;
        }

        public static bool RemoveCustomScope(object owner)
        {
#if UNITY_EDITOR
            ScopeKeyValidation(owner);
#endif
            return DefaultInstance.RemoveScope(owner);
        }

        private static void ScopeKeyValidation(object owner)
        {
            if (owner is GameObject)
            {
                throw new Exception("GameObjects can not be used as custom scope key.");
            }

            if (owner is Scene)
            {
                throw new Exception("Scenes can not be used as custom scope key.");
            }
            
            if (owner == DefaultInstance)
            {
                throw new Exception("Global Scope key can not be used as custom scope key.");
            }
        }
        
        public static Scope ScopeOf(object owner, bool createIfNotExist = true)
        {
            if (owner is GameObject gameObject)
            {
                return GameObjectScope(gameObject, createIfNotExist);
            }

            if (owner is Scene scene)
            {
                return SceneScope(scene, createIfNotExist);
            }
            
            if (owner == DefaultInstance)
            {
                return GlobalScope(createIfNotExist);
            }
            
            return CustomScope(owner, createIfNotExist);
        }
        
        public static void Add(ServiceIdentifier identifier, object service, Scope scope = default)
        {
            scope ??= GlobalScope();
            scope.Add(identifier, service);
        }

        public static void AddSingle(object service, Scope scope = default)
        {
            Add(new ServiceIdentifier(service.GetType()), service, scope);
        }
        
        public static void AddSingle(Type type, object service, Scope scope = default)
        {
            Add(new ServiceIdentifier(type), service, scope);
        }
        
        public static void AddSingle<T>(T service, Scope scope = default)
        {
            Add(new ServiceIdentifier(typeof(T)), service, scope);
        }
        
        public static void AddTagged(object service, object tag, Scope scope = default)
        {
            Add(new ServiceIdentifier(service.GetType(), tag), service, scope);
        }
        
        public static void AddTagged(Type type, object service, object tag, Scope scope = default)
        {
            Add(new ServiceIdentifier(type, tag), service, scope);
        }
        
        public static void AddTagged<T>(T service, object tag, Scope scope = default)
        {
            Add(new ServiceIdentifier(service.GetType(), tag), service, scope);
        }
        
        public static void AddWithId(object service, long id, Scope scope = default)
        {
            Add(new ServiceIdentifier(service.GetType(), id), service, scope);
        }
        
        public static void AddWithId(Type type, object service, long id, Scope scope = default)
        {
            Add(new ServiceIdentifier(type, id), service, scope);
        }
        
        public static void AddWithId<T>(T service, long id, Scope scope = default)
        {
            Add(new ServiceIdentifier(typeof(T), id), service, scope);
        }
        
        public static void AddTaggedWithId(object service, object tag, long id, Scope scope = default)
        {
            Add(new ServiceIdentifier(service.GetType(), tag, id), service, scope);
        }
        
        public static void AddTaggedWithId(Type type, object service, object tag, long id, Scope scope = default)
        {
            Add(new ServiceIdentifier(type, tag, id), service, scope);
        }
        
        public static void AddTaggedWithId<T>(T service, object tag, long id, Scope scope = default)
        {
            Add(new ServiceIdentifier(typeof(T), tag, id), service, scope);
        }
        
        public static bool Remove(ServiceIdentifier identifier, Scope scope = default)
        {
            scope ??= GlobalScope();
            return scope.Remove(identifier);
        }
        
        public static void RemoveSingle(object service, Scope scope = default)
        {
            Remove(new ServiceIdentifier(service.GetType()), scope);
        }
        
        public static void RemoveSingle(Type type, Scope scope = default)
        {
            Remove(new ServiceIdentifier(type), scope);
        }
        
        public static void RemoveSingle<T>(T service, Scope scope = default)
        {
            Remove(new ServiceIdentifier(typeof(T)), scope);
        }
        
        public static void RemoveTagged(object service, object tag, Scope scope = default)
        {
            Remove(new ServiceIdentifier(service.GetType(), tag), scope);
        }
        
        public static void RemoveTagged(Type type, object tag, Scope scope = default)
        {
            Remove(new ServiceIdentifier(type, tag), scope);
        }
        
        public static void RemoveTagged<T>(object tag, Scope scope = default)
        {
            Remove(new ServiceIdentifier(typeof(T), tag), scope);
        }
        
        public static void RemoveWithId(object service, long id, Scope scope = default)
        {
            Remove(new ServiceIdentifier(service.GetType(), id), scope);
        }
        
        public static void RemoveWithId(Type type, long id, Scope scope = default)
        {
            Remove(new ServiceIdentifier(type, id), scope);
        }
        
        public static void RemoveWithId<T>(long id, Scope scope = default)
        {
            Remove(new ServiceIdentifier(typeof(T), id), scope);
        }
        
        public static void RemoveTaggedWithId(object service, object tag, long id, Scope scope = default)
        {
            Remove(new ServiceIdentifier(service.GetType(), tag, id), scope);
        }
        
        public static void RemoveTaggedWithId(Type type, object tag, long id, Scope scope = default)
        {
            Remove(new ServiceIdentifier(type, tag, id), scope);
        }
        
        public static void RemoveTaggedWithId<T>(object tag, long id, Scope scope = default)
        {
            Remove(new ServiceIdentifier(typeof(T), tag, id), scope);
        }
        
        public static object Get(ServiceIdentifier identifier, Scope scope = default)
        {
            scope ??= GlobalScope();
            return scope.Get(identifier);
        }
        
        public static bool TryGet(ServiceIdentifier identifier, out object service, Scope scope = default)
        {
            scope ??= GlobalScope();
            return scope.TryGet(identifier, out service);
        }
        
        public static object GetOrDefault(ServiceIdentifier identifier, object defaultValue = default, Scope scope = default)
        {
            return TryGet(identifier, out object value, scope) 
                ? value 
                : defaultValue;
        }
        
        public static object GetSingle(Type type, Scope scope = default)
        {
            return Get(new ServiceIdentifier(type), scope);
        }
        
        public static object GetTagged(Type type, object tag, Scope scope = default)
        {
            return Get(new ServiceIdentifier(type, tag), scope);
        }
        
        public static object GetWithId(Type type ,long id, Scope scope = default)
        {
            return Get(new ServiceIdentifier(type, id), scope);
        }
        
        public static object GetTaggedWithId(Type type, object tag, long id, Scope scope = default)
        {
            return Get(new ServiceIdentifier(type, tag, id), scope);
        }
        
        public static T GetSingle<T>(Scope scope = default)
        {
            return (T)Get(new ServiceIdentifier(typeof(T)), scope);
        }
        
        public static T GetTagged<T>(object tag, Scope scope = default)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), tag), scope);
        }
        
        public static T GetWithId<T>(long id, Scope scope = default)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), id), scope);
        }
        
        public static T GetTaggedWithId<T>(object tag, long id , Scope scope = default)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), tag, id), scope);
        }
        
        public static bool TryGetSingle(Type serviceType, out object service, Scope scope = default)
        {
            return TryGet(new ServiceIdentifier(serviceType), out service, scope);
        }
        
        public static bool TryGetTagged(Type serviceType, object tag, out object service, Scope scope = default)
        {
            return TryGet(new ServiceIdentifier(serviceType, tag), out service, scope);
        }
        
        public static bool TryGetWithId(Type serviceType, long id, out object service, Scope scope = default)
        {
            return TryGet(new ServiceIdentifier(serviceType, id), out service, scope);
        }
        
        public static bool TryGetTaggedWithId(Type serviceType, object tag, long id, out object service, Scope scope = default)
        {
            return TryGet(new ServiceIdentifier(serviceType, tag, id), out service, scope);
        }

        public static bool TryGetSingle<T>(out T service, Scope scope = default)
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T)), out var value, scope);
            service = (T)value;
            return result;
        }
        
        public static bool TryGetTagged<T>(object tag, out object service, Scope scope = default)
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), tag), out var value, scope);
            service = (T)value;
            return result;
        }
        
        public static bool TryGetWithId<T>(long id, out object service, Scope scope = default) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), id), out var value, scope);
            service = (T)value;
            return result;
        }
        
        public static bool TryGetTaggedWithId<T>(object tag, long id, out object service, Scope scope = default)
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), tag, id), out var value, scope);
            service = (T)value;
            return result;
        }
        
        public static object GetSingleOrDefault(Type type, object defaultValue = default, Scope scope = default)
        {
            return TryGetSingle(type, out object value, scope) 
                ? value 
                : defaultValue;
        }
        
        public static object GetTaggedOrDefault(Type type, object tag, object defaultValue = default, Scope scope = default)
        {
            return TryGetTagged(type, tag, out object value, scope) 
                ? value 
                : defaultValue;
        }
        
        public static object GetWithIdOrDefault(Type type, long id, object defaultValue = default, Scope scope = default)
        {
            return TryGetWithId(type, id, out object value, scope) 
                ? value 
                : defaultValue;
        }
        
        public static object GetTaggedWithIdOrDefault(Type type, object tag, long id, object defaultValue = default, Scope scope = default)
        {
            return TryGetTaggedWithId(type, tag, id, out object value, scope) 
                ? value 
                : defaultValue;
        }
        
        public static T GetSingleOrDefault<T>(object defaultValue = default, Scope scope = default)
        {
            return TryGetSingle(typeof(T), out object value, scope) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public static T GetTaggedOrDefault<T>(object tag, object defaultValue = default, Scope scope = default)
        {
            return TryGetTagged(typeof(T), tag, out object value, scope) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public static T GetWithIdOrDefault<T>(long id, object defaultValue = default, Scope scope = default)
        {
            return TryGetWithId(typeof(T), id, out object value, scope) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public static T GetTaggedWithIdOrDefault<T>(object tag, long id, object defaultValue = default, Scope scope = default)
        {
            return TryGetTaggedWithId(typeof(T), tag, id, out object value, scope) 
                ? (T)value 
                : (T)defaultValue;
        }

        public static void Reset()
        {
            DefaultInstance.DestroyComponents();
            DefaultInstance = null;
        }
    }
}

public interface IScopyEditorCustomGUI
{
    void OnScopyEditorGUI();
}