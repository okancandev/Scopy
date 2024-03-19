using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScope : MonoBehaviour
{
    private void OnDestroy()
    {
        Scopy.RemoveGlobalScope();
    }
}
