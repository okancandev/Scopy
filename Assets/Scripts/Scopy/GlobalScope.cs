using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class GlobalScope : MonoBehaviour
{
    private void OnDestroy()
    {
        if(Scopy.Quiting)
            return;
        
        Scopy.RemoveGlobalScope();
    }
}
