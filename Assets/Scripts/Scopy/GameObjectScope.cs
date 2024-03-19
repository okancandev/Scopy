using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectScope : MonoBehaviour
{
    private void OnDestroy()
    {
        Scopy.RemoveGameObjectScope(gameObject);
    }
}
