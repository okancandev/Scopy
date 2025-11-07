using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Okancandev.Scopy
{
    public sealed class UScopesInstance : IDisposable
    {
        private readonly Dictionary<object, Scope> _scopes = new();
        private readonly Dictionary<Scope, ScopeTracker> _activeComponents = new();

        public IReadOnlyDictionary<object, Scope> Scopes => _scopes;
        public object GlobalScopeKey => this;
 
        public Scope GlobalScope(bool createIfNotExist = true)
        {
            if (!TryGetScope(GlobalScopeKey, out var scope))
            {
                if (createIfNotExist)
                    scope = CreateScope(GlobalScopeKey);
                else
                    return null;
            }
            if (!_activeComponents.TryGetValue(scope, out _))
            {
                var globalScopeObject = new GameObject();
                globalScopeObject.name = "GlobalScopeTracker";
                globalScopeObject.AddComponent<AutoGlobalScopeTracker>();
                GameObject.DontDestroyOnLoad(globalScopeObject);
            }
            return scope;
        }
        
        public Scope SceneScope(Scene scene, bool createIfNotExist = true)
        {
            if (!TryGetScope(scene, out var scope))
            {
                if (createIfNotExist)
                    scope = CreateScope(scene);
                else
                    return null;
            }
            if (!_activeComponents.TryGetValue(scope, out _))
            {
                var sceneScopeObject = new GameObject();
                sceneScopeObject.name = "SceneScopeTracker";
                sceneScopeObject.AddComponent<AutoSceneScopeTracker>();
                SceneManager.MoveGameObjectToScene(sceneScopeObject, scene);
            }
            return scope;
        }
        
        public Scope ActiveSceneScope(bool createIfNotExist = true)
        {
            return SceneScope(SceneManager.GetActiveScene(), createIfNotExist);
        }
        
        public Scope GameObjectScope(GameObject gameObject, bool createIfNotExist = true)
        {
            if (!TryGetScope(gameObject, out var scope))
            {
                if (createIfNotExist)
                    scope = CreateScope(gameObject);
                else
                    return null;
            }
            if (!_activeComponents.TryGetValue(scope, out _))
            {
                gameObject.AddComponent<AutoGameObjectScopeTracker>();
            }
            return scope;
        }
        
        public Scope GetOrCreateScope(object owner)
        {
            if (_scopes.TryGetValue(owner, out Scope scope))
            {
                return scope;
            }

            var newScope = new Scope();
            _scopes.Add(owner, newScope);
            return newScope;
        }

        public Scope CreateScope(object owner)
        {
            var newScope = new Scope();
            _scopes.Add(owner, newScope);
            return newScope;
        }
        
        public void AddScope(object owner, Scope scope)
        {
            _scopes.Add(owner, scope);
        }

        public bool HasScope(object owner)
        {
            return _scopes.ContainsKey(owner);
        }

        public bool TryGetScope(object owner, out Scope scope)
        {
            return _scopes.TryGetValue(owner, out scope);
        }

        public bool RemoveScope(object owner)
        {
            return _scopes.Remove(owner);
        }
        
        public void RegisterTrackerComponent(Scope scope, ScopeTracker lifecycle)
        {
            _activeComponents.Add(scope, lifecycle);
        }
        
        public void RemoveTrackerComponent(ScopeTracker lifecycle)
        {
            var owner = lifecycle.GetOwnerObject();
            var scope = _scopes[owner];
            RemoveScope(owner);
            _activeComponents.Remove(scope);
        }

        public bool TryGetScopeComponent(Scope scope, out ScopeTracker scopeTracker)
        {
            if (_activeComponents.TryGetValue(scope, out var value))
            {
                scopeTracker = value;
                return true;
            }

            scopeTracker = null;
            return false;
        }

        public object FindOwner(Scope scope)
        {
            if (_activeComponents.TryGetValue(scope, out var component))
            {
                return component.GetOwnerObject();
            }

            foreach (var pair in _scopes)
            {
                if (pair.Value == scope)
                    return pair.Key;
            }

            return null;
        }

        public void Destroy()
        {
            foreach (var scopyComponent in _activeComponents.Values)
            {
                scopyComponent.DetachScopyInstance(); //this ensures collection not modified in foreach
                scopyComponent.DestroySelfImmediate();
            }
            _activeComponents.Clear();
            _scopes.Clear();
        }
        
        public void Dispose()
        {
            Destroy();
        }
    }
}


