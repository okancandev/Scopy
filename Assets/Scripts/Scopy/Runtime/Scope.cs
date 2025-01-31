using System;
using System.Collections.Generic;

namespace Okancandev.Scopy
{
    public class Scope
    {
        public IReadOnlyDictionary<ServiceIdentifier, object> Services => _services;
        private readonly Dictionary<ServiceIdentifier, object> _services = new();

        public void Add(ServiceIdentifier identifier, object service) 
        {
            _services.Add(identifier, service);
        }

        public void AddSingle(object service)
        {
            Add(new ServiceIdentifier(service.GetType()), service);
        }
        
        public void AddWithId(object service, long id)
        {
            Add(new ServiceIdentifier(service.GetType(), id), service);
        }
        
        public void AddTagged(object service, object tag)
        {
            Add(new ServiceIdentifier(service.GetType(), tag), service);
        }

        public void AddTaggedWithId(object service, long id, object tag)
        {
            Add(new ServiceIdentifier(service.GetType(), id, tag), service);
        }
        
        public bool Remove(ServiceIdentifier identifier) 
        {
            return _services.Remove(identifier);
        }
        
        public void RemoveSingle(object service)
        {
            Remove(new ServiceIdentifier(service.GetType()));
        }
        
        public void RemoveWithId(object service, long id)
        {
            Remove(new ServiceIdentifier(service.GetType(), id));
        }
        
        public void RemoveTagged(object service, object tag)
        {
            Remove(new ServiceIdentifier(service.GetType(), tag));
        }

        public void RemoveTaggedWithId(object service, long id, object tag)
        {
            Remove(new ServiceIdentifier(service.GetType(), id, tag));
        }
        
        public object Get(ServiceIdentifier identifier) 
        {
            return _services[identifier];
        }
        
        public bool TryGet(ServiceIdentifier identifier, out object service) 
        {
            return _services.TryGetValue(identifier, out service);
        }
        
        public object GetOrDefault(ServiceIdentifier identifier, object defaultValue = default) 
        {
            return TryGet(identifier, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetSingle(Type type)
        {
            return Get(new ServiceIdentifier(type));
        }
        
        public object GetWithId(Type type ,long id)
        {
            return Get(new ServiceIdentifier(type, id));
        }
        
        public object GetTagged(Type type, object tag)
        {
            return Get(new ServiceIdentifier(type, tag));
        }
        
        public object GetTaggedWithId(Type type, long id, object tag)
        {
            return Get(new ServiceIdentifier(type, id, tag));
        }
        
        public T GetSingle<T>()
        {
            return (T)Get(new ServiceIdentifier(typeof(T)));
        }
        
        public T GetWithId<T>(long id)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), id));
        }
        
        public T GetTagged<T>(object tag)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), tag));
        }
        
        public T GetTaggedWithId<T>(long id, object tag)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), id, tag));
        }
        
        public bool TryGetSingle(Type serviceType, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType), out service);
        }
        
        public bool TryGetWithId(Type serviceType, long id, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, id), out service);
        }
        
        public bool TryGetTagged(Type serviceType, object tag, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, tag), out service);
        }
        
        public bool TryGetTaggedWithId(Type serviceType, long id, object tag, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, id, tag), out service);
        }

        public bool TryGetSingle<T>(out T service)
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T)), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetWithId<T>(long id, out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), id), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetTagged<T>(object tag, out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), tag), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetTaggedWithId<T>(long id, object tag, out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), id, tag), out var value);
            service = (T)value;
            return result;
        }
        
        public object GetSingleOrDefault(Type type, object defaultValue = default)
        {
            return TryGetSingle(type, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetWithIdOrDefault(Type type, long id, object defaultValue = default)
        {
            return TryGetWithId(type, id, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedOrDefault(Type type, object tag, object defaultValue = default)
        {
            return TryGetTagged(type, tag, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedWithIdOrDefault(Type type, long id, object tag, object defaultValue = default)
        {
            return TryGetTaggedWithId(type, id, tag, out object value) 
                ? value 
                : defaultValue;
        }
        
        public T GetSingleOrDefault<T>(object defaultValue = default)
        {
            return TryGetSingle(typeof(T), out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetWithIdOrDefault<T>(long id, object defaultValue = default)
        {
            return TryGetWithId(typeof(T), id, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetTaggedOrDefault<T>(object tag, object defaultValue = default)
        {
            return TryGetTagged(typeof(T), tag, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetTaggedWithIdOrDefault<T>(long id, object tag, object defaultValue = default)
        {
            return TryGetTaggedWithId(typeof(T), id, tag, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
    }
}