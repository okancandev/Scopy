using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scopy
{
    private static Scope _globalScope;
    private static Dictionary<Scene, Scope> _sceneScopes;
    private static Dictionary<GameObject, Scope> _gameObjectScopes;

    public static bool Quiting { get; private set; }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        Application.quitting += OnQuit;
    }

    private static void OnQuit()
    {
        Quiting = true;
    }

    public static Scope GetGlobalScope()
    {
        if (_globalScope != null)
            return _globalScope;
        
        var newScope = new Scope();
        var globalScopeObject = new GameObject();
        globalScopeObject.name = "GlobalScope";
        globalScopeObject.AddComponent<GlobalScope>();
        GameObject.DontDestroyOnLoad(globalScopeObject);
        _globalScope = newScope;
        return newScope;
    }
    
    public static void RemoveGlobalScope()
    {
        _globalScope = new Scope();
    }
    
    public static Scope GetSceneScope(Scene scene)
    {
        if (_sceneScopes == null)
            _sceneScopes = new Dictionary<Scene, Scope>();
        
        if (_sceneScopes.TryGetValue(scene, out var scope))
            return scope;

        var newScope = new Scope();
        var sceneScopeObject = new GameObject();
        sceneScopeObject.name = "SceneScope";
        sceneScopeObject.AddComponent<SceneScope>();
        SceneManager.MoveGameObjectToScene(sceneScopeObject, scene);
        _sceneScopes.Add(scene, newScope);
        return newScope;
    }

    internal static bool RemoveSceneScope(Scene scene)
    {
        return _sceneScopes.Remove(scene);
    }
    
    public static Scope GetGameObjectScope(GameObject gameObject)
    {
        if (_gameObjectScopes == null)
            _gameObjectScopes = new Dictionary<GameObject, Scope>();
        
        if (_gameObjectScopes.TryGetValue(gameObject, out var scope))
            return scope;

        var newScope = new Scope();
        gameObject.AddComponent<GameObjectScope>();
        _gameObjectScopes.Add(gameObject, newScope);
        return newScope;
    }

    internal static bool RemoveGameObjectScope(GameObject gameObject)
    {
        return _gameObjectScopes.Remove(gameObject);
    }
}

public static class ScopySceneExtensions
{
    public static Scope GetScope(this Scene scene)
    {
        return Scopy.GetSceneScope(scene);
    }
    
    public static Scope GetGlobalScope(this Scene gameObject)
    {
        return Scopy.GetGlobalScope();
    }
}

public static class ScopyGameObjectExtensions
{
    public static Scope GetSceneScope(this GameObject gameObject)
    {
        return Scopy.GetSceneScope(gameObject.scene);
    }
    
    public static Scope GetScope(this GameObject gameObject)
    {
        return Scopy.GetGameObjectScope(gameObject);
    }

    public static Scope GetGlobalScope(this GameObject gameObject)
    {
        return Scopy.GetGlobalScope();
    }
}