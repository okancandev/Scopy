using UnityEngine;
using UnityEngine.SceneManagement;

namespace Okancandev.Scopy
{
    public static class ScopySceneExtensions
    {
        public static Scope GetScope(this Scene scene)
        {
            return Scopy.GetSceneScope(scene);
        }

        public static Scope GetGlobalScope(this Scene scene)
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
}