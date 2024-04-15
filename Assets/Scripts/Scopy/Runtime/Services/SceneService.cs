using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scopy.User
{
    public abstract class SceneService : MonoBehaviour
    {
        private bool _awake;
        
        public bool IsAwake() => _awake;
    
        protected void Awake()
        {
            _awake = true;
            gameObject.GetSceneScope().Add(this);
            OnAwake();
        }
        
        protected virtual void OnAwake()
        {
            
        }
    
        protected T GetService<T>()
        {
            var sceneScopeResult = gameObject.GetSceneScope().Get<T>();
            if (sceneScopeResult != null)
                return sceneScopeResult;
        
            return gameObject.GetGlobalScope().Get<T>();
        }

        protected void OnDestroy()
        {
            if (_awake && !Scopy.Quiting)
            {
                gameObject.GetSceneScope().Remove(this);
                OnSafeDestroy();
            }
        }
        
        protected virtual void OnSafeDestroy()
        {
            
        }
    }
}