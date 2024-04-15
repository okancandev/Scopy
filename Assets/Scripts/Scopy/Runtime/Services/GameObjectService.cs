using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy.User
{
    public abstract class GameObjectService : MonoBehaviour
    {
        private bool _awake;

        public bool IsAwake() => _awake;
    
        private void Awake()
        {
            _awake = true;
            gameObject.GetScope().Add(this);
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }
    
        protected T GetService<T>()
        {
            var gameObjectScopeResult = gameObject.GetScope().Get<T>();
            if (gameObjectScopeResult != null)
                return gameObjectScopeResult;
        
            var sceneScopeResult = gameObject.GetSceneScope().Get<T>();
            if (sceneScopeResult != null)
                return sceneScopeResult;
        
            return gameObject.GetGlobalScope().Get<T>();
        }

        private void OnDestroy()
        {
            if (_awake && !Scopy.Quiting)
            {
                gameObject.GetScope().Remove(this);
                OnSafeDestroy();
            }
        }
        
        protected virtual void OnSafeDestroy()
        {
            
        }
    }
}