using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scopy
{
    [AddComponentMenu("")]
    public class SceneScope : MonoBehaviour
    {
        private void OnDestroy()
        {
            if(Scopy.Quiting)
                return;
        
            Scopy.RemoveSceneScope(gameObject.scene);
        }
    }
}