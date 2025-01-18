using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Okancandev.Scopy
{
    public class ScopyManager
    {
        private readonly Dictionary<object, Scope> _scopes = new();
        private readonly Dictionary<Scope, ScopeTracker> _activeComponents = new();

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

        public bool HasScope(object owner)
        {
            return _scopes.ContainsKey(owner);
        }
        
        public void AddScope(object owner, Scope scope)
        {
            _scopes.Add(owner, scope);
        }

        public bool RemoveScope(object owner)
        {
            return _scopes.Remove(owner);
        }
        
        public void RegisterComponent(Scope scope, ScopeTracker lifecycle)
        {
            _activeComponents.Add(scope, lifecycle);
        }
        
        public void RemoveComponent(ScopeTracker lifecycle)
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

        public void DestroyComponents()
        {
            foreach (var scopyComponent in _activeComponents.Values)
            {
                scopyComponent.DestroySelf();
            }
        }
    }
}


