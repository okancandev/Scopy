using UnityEngine;
using UnityEngine.SceneManagement;

namespace Okancandev.Scopy
{
    public static class ScopySceneExtensions
    {
        public static ScopyInstance ScopyInstance(this Scene scene)
        {
            return Scopy.DefaultInstance;
        }
        
        public static Scope Scope(this Scene scene, bool createIfNotExist = true)
        {
            return Scopy.SceneScope(scene, createIfNotExist);
        }

        public static Scope GlobalScope(this Scene scene, bool createIfNotExist = true)
        {
            return Scopy.GlobalScope(createIfNotExist);
        }
        
        public static HierarchicScope HierarchicScope(this Scene scene)
        {
            return new HierarchicScope(scene);
        }
    }

    public static class ScopyGameObjectExtensions
    {
        public static ScopyInstance ScopyInstance(this GameObject gameObject)
        {
            return Scopy.DefaultInstance;
        }
        
        public static Scope Scope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return Scopy.GameObjectScope(gameObject, createIfNotExist);
        }
        
        public static Scope SceneScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return Scopy.SceneScope(gameObject.scene, createIfNotExist);
        }
        
        public static Scope ActiveSceneScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return Scopy.ActiveSceneScope(createIfNotExist);
        }

        public static Scope GlobalScope(this GameObject gameObject, bool createIfNotExist = true)
        {
            return Scopy.GlobalScope(createIfNotExist);
        }
        
        public static HierarchicScope HierarchicScope(this GameObject gameObject)
        {
            return new HierarchicScope(gameObject);
        }
    }
}