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
        internal static ScopyManager _defaultInstance;

        public static ScopyManager DefaultInstance
        {
            get => _defaultInstance ??= new ScopyManager();
            private set => _defaultInstance = value;
        }

        public static Scope GlobalScope()
        {
            var scope =  DefaultInstance.GetOrCreateScope(DefaultInstance);
            if (!DefaultInstance.TryGetScopeComponent(scope, out _))
            {
                var globalScopeObject = new GameObject();
                globalScopeObject.name = "GlobalScopeTracker";
                globalScopeObject.AddComponent<AutoGlobalScopeTracker>();
                GameObject.DontDestroyOnLoad(globalScopeObject);
            }
            return scope;
        }
        
        public static Scope SceneScope(Scene scene)
        {
            var scope =  DefaultInstance.GetOrCreateScope(scene);
            if (!DefaultInstance.TryGetScopeComponent(scope, out _))
            {
                var sceneScopeObject = new GameObject();
                sceneScopeObject.name = "SceneScopeTracker";
                sceneScopeObject.AddComponent<AutoSceneScopeTracker>();
                SceneManager.MoveGameObjectToScene(sceneScopeObject, scene);
            }
            return DefaultInstance.GetOrCreateScope(scene);
        }
        
        public static Scope GameObjectScope(GameObject gameObject)
        {
            var scope =  DefaultInstance.GetOrCreateScope(gameObject);
            if (!DefaultInstance.TryGetScopeComponent(scope, out _))
            {
                gameObject.AddComponent<AutoGameObjectScopeTracker>();
            }
            return DefaultInstance.GetOrCreateScope(gameObject);
        }
        
        public static Scope CustomScope(object owner)
        {
#if UNITY_EDITOR
            //warn about not to use null, Scene or Gameobject in here
#endif
            return DefaultInstance.GetOrCreateScope(owner);
        }

        public static bool RemoveCustomScope(object owner)
        {
#if UNITY_EDITOR
            //warn about not to use null, Scene or Gameobject in here
#endif
            return DefaultInstance.RemoveScope(owner);
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