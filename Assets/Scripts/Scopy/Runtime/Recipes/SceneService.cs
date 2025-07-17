using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy.Recipes
{
    public abstract class SceneService : Service
    {
        protected override Scope GetScope(bool createIfNotExist = true)
        {
            return gameObject.SceneScope(createIfNotExist);
        }
    }
}