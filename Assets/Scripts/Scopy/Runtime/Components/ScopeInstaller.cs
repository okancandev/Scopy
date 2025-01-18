using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Okancandev.Scopy
{
    public class ScopeInstaller : MonoBehaviour
    {
        [SerializeField] public Object[] SingleServices;
        [SerializeField] public StringTaggedSerializedIdentifier[] StringTaggedMultipleServices;
        [SerializeField] public UnityObjectTaggedSerializedIdentifier[] UnityObjectTaggedMultipleServices;
   
        public virtual void Install(Scope scope)
        {
            foreach (var service in SingleServices)
            {
                scope.AddSingle(service);
            }
            foreach (var customRegister in StringTaggedMultipleServices)
            {
                var identifier = new ServiceIdentifier(customRegister.Service.GetType(), customRegister.Id, customRegister.Tag);
                scope.Add(identifier, customRegister.Service);
            }
            
            foreach (var customRegister in UnityObjectTaggedMultipleServices)
            {
                var identifier = new ServiceIdentifier(customRegister.Service.GetType(), customRegister.Id, customRegister.Tag);
                scope.Add(identifier, customRegister.Service);
            }
        }
    }
}

[Serializable]
public class StringTaggedSerializedIdentifier
{
    public Object Service;
    public long Id;
    public string Tag;
}

[Serializable]
public class UnityObjectTaggedSerializedIdentifier
{
    public Object Service;
    public long Id;
    public Object Tag;
}