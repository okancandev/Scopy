using UnityEngine;
using UnityEngine.SceneManagement;

namespace Okancandev.UScopes
{
    public static class UScopesSceneExtensions
    {
        public static UScopesInstance UScopesInstance(this Scene scene)
        {
            return UScopes.DefaultInstance;
        }
        
        public static Scope Scope(this Scene scene, bool createIfNotExist = true)
        {
            return UScopes.Scene(scene, createIfNotExist);
        }

        public static Scope GlobalScope(this Scene scene, bool createIfNotExist = true)
        {
            return UScopes.Global(createIfNotExist);
        }
        
        public static HierarchicScope HierarchicScope(this Scene scene)
        {
            return new HierarchicScope(scene);
        }
    }

    public static class UScopesGameObjectExtensions
    {
        public static UScopesInstance UScopesInstance(this GameObject gameObject)
        {
            return UScopes.DefaultInstance;
        }
        
        public static Scope Scope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return UScopes.GameObject(gameObject, createIfNotExist);
        }
        
        public static Scope SceneScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return UScopes.Scene(gameObject.scene, createIfNotExist);
        }
        
        public static Scope ActiveSceneScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return UScopes.ActiveScene(createIfNotExist);
        }

        public static Scope GlobalScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return UScopes.Global(createIfNotExist);
        }
        
        public static HierarchicScope HierarchicScope(this GameObject gameObject)
        {
            return new HierarchicScope(gameObject);
        }
    }
}