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
            return UScopes.SceneScope(scene, createIfNotExist);
        }

        public static Scope GlobalScope(this Scene scene, bool createIfNotExist = true)
        {
            return UScopes.GlobalScope(createIfNotExist);
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
            return UScopes.GameObjectScope(gameObject, createIfNotExist);
        }
        
        public static Scope SceneScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return UScopes.SceneScope(gameObject.scene, createIfNotExist);
        }
        
        public static Scope ActiveSceneScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return UScopes.ActiveSceneScope(createIfNotExist);
        }

        public static Scope GlobalScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return UScopes.GlobalScope(createIfNotExist);
        }
        
        public static HierarchicScope HierarchicScope(this GameObject gameObject)
        {
            return new HierarchicScope(gameObject);
        }
    }
}