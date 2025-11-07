using System;
using UnityEngine;

namespace Okancandev.UScopes.Recipes
{
    public abstract class GlobalScopedMonoBehaviour : ScopedMonoBehaviour
    {
        protected override Scope GetScope(bool createIfNotExist = true)
        {
            return gameObject.GlobalScope(createIfNotExist);
        }
    }
}