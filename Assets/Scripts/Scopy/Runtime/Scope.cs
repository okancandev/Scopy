using System;
using System.Collections.Generic;

namespace Okancandev.Scopy
{
    public class Scope
    {
        private readonly Dictionary<Type, object> _services = new();
    
        public IReadOnlyDictionary<Type, object> Services => _services;

        public void Add(Type type, object service) 
        {
            _services.Add(type, service);
        }
    
        public void Add(object service)
        {
            _services.Add(service.GetType(), service);
        }
    
        public bool Remove(object obj) 
        {
            return _services.Remove(obj.GetType());
        }

        public bool Remove(Type type) 
        {
            return _services.Remove(type);
        }

        public object Get(Type serviceType) 
        {
            return _services[serviceType];
        }

        public T Get<T>()
        {
            return (T)_services[typeof(T)];
        }
        
        public object GetOrDefault(Type serviceType, object @default = default)
        {
            return _services.TryGetValue(serviceType, out object value) 
                ? value 
                : @default;
        }

        public T GetOrDefault<T>(T @default = default) where T : class
        {
            return TryGet(out T value) 
                ? value 
                : @default;
        }
        
        public bool TryGet(Type serviceType, out object service) 
        {
            return _services.TryGetValue(serviceType, out service);
        }

        public bool TryGet<T>(out T service) where T : class
        {
            bool result = _services.TryGetValue(typeof(T), out var value);
            service = (T)value;
            return result;
        }
    }
}