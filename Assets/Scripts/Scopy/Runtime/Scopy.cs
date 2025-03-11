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
        internal static ScopyInstance _defaultInstance;

        public static ScopyInstance DefaultInstance
        {
            get => _defaultInstance ??= new ScopyInstance();
            private set => _defaultInstance = value;
        }

        public static Scope GlobalScope(bool createIfNotExist = true)
        {
            return DefaultInstance.GlobalScope(createIfNotExist);
        }
        
        public static Scope SceneScope(Scene scene, bool createIfNotExist = true)
        {
            return DefaultInstance.SceneScope(scene, createIfNotExist);
        }
        
        public static Scope GameObjectScope(GameObject gameObject, bool createIfNotExist = true)
        {
            return DefaultInstance.GameObjectScope(gameObject, createIfNotExist);
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