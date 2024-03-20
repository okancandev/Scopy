using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectService : MonoBehaviour
{
    private bool _awake;
    
    protected void Awake()
    {
        _awake = true;
        gameObject.GetScope().Add(this);
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

    protected void OnDestroy()
    {
        if (_awake && !Scopy.Quiting)
            gameObject.GetScope().Remove(this);
    }
}
