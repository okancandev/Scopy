using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneService : MonoBehaviour
{
    private bool _awake;
    
    protected void Awake()
    {
        _awake = true;
        gameObject.GetSceneScope().Add(this);
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
            gameObject.GetSceneScope().Remove(this);
    }
}
