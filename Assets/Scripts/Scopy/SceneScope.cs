using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScope : MonoBehaviour
{
    private void OnDestroy()
    {
        Scopy.RemoveSceneScope(gameObject.scene);
    }
}