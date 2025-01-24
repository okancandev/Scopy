using UnityEngine;
using UnityEngine.SceneManagement;

namespace Okancandev.Scopy
{
    public static class ScopySceneExtensions
    {
        public static Scope Scope(this Scene scene)
        {
            return Scopy.SceneScope(scene);
        }

        public static Scope GlobalScope(this Scene scene)
        {
            return Scopy.GlobalScope();
        }
        
        public static HierarchicScope HierarchicScope(this Scene scene)
        {
            return new HierarchicScope(scene);
        }
    }

    public static class ScopyGameObjectExtensions
    {
        public static Scope Scope(this GameObject gameObject)
        {
            return Scopy.GameObjectScope(gameObject);
        }
        
        public static Scope SceneScope(this GameObject gameObject)
        {
            return Scopy.SceneScope(gameObject.scene);
        }

        public static Scope GlobalScope(this GameObject gameObject)
        {
            return Scopy.GlobalScope();
        }
        
        public static HierarchicScope HierarchicScope(this GameObject gameObject)
        {
            return new HierarchicScope(gameObject);
        }
    }
}