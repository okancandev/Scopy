using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.UScopes.Recipes
{
    public abstract class GameObjectService : Service
    {
        protected override Scope GetScope(bool createIfNotExist = true)
        {
            return gameObject.Scope(createIfNotExist);
        }
    }
}
