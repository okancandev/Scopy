using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Okancandev.Scopy.Ideas
{
    public abstract class GameObjectService : Service
    {
        protected override Scope GetScope(bool createIfNotExist = true)
        {
            return gameObject.Scope(createIfNotExist);
        }
    }
}
