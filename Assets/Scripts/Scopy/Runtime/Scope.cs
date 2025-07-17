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
        
        public void AddSingle(Type type, object service)
        {
            Add(new ServiceIdentifier(type), service);
        }
        
        public void AddSingle<T>(T service)
        {
            Add(new ServiceIdentifier(typeof(T)), service);
        }
        
        public void AddTagged(Type type, object service, object tag)
        {
            Add(new ServiceIdentifier(type, tag), service);
        }
        
        public void AddTagged<T>(T service, object tag)
        {
            Add(new ServiceIdentifier(typeof(T), tag), service);
        }
        
        public void AddTagged(object service, object tag)
        {
            Add(new ServiceIdentifier(service.GetType(), tag), service);
        }
        
        public void AddWithId(object service, long id)
        {
            Add(new ServiceIdentifier(service.GetType(), id), service);
        }
        
        public void AddWithId(Type type, object service, long id)
        {
            Add(new ServiceIdentifier(type, id), service);
        }
        
        public void AddWithId<T>(T service, long id)
        {
            Add(new ServiceIdentifier(typeof(T), id), service);
        }

        public void AddTaggedWithId(object service, object tag, long id)
        {
            Add(new ServiceIdentifier(service.GetType(), tag, id), service);
        }
        
        public void AddTaggedWithId(Type type, object service, object tag, long id)
        {
            Add(new ServiceIdentifier(type, tag, id), service);
        }
        
        public void AddTaggedWithId<T>(T service, object tag, long id)
        {
            Add(new ServiceIdentifier(typeof(T), tag, id), service);
        }
        
        public bool Remove(ServiceIdentifier identifier) 
        {
            return _services.Remove(identifier);
        }
        
        public void RemoveSingle(object service)
        {
            Remove(new ServiceIdentifier(service.GetType()));
        }
        
        public void RemoveSingle(Type type)
        {
            Remove(new ServiceIdentifier(type));
        }
        
        public void RemoveSingle<T>()
        {
            Remove(new ServiceIdentifier(typeof(T)));
        }
        
        public void RemoveTagged(object service, object tag)
        {
            Remove(new ServiceIdentifier(service.GetType(), tag));
        }
        
        public void RemoveTagged(Type type, object tag)
        {
            Remove(new ServiceIdentifier(type, tag));
        }
        
        public void RemoveTagged<T>(object tag)
        {
            Remove(new ServiceIdentifier(typeof(T), tag));
        }
        
        public void RemoveWithId(object service, long id)
        {
            Remove(new ServiceIdentifier(service.GetType(), id));
        }
        
        public void RemoveWithId(Type type, long id)
        {
            Remove(new ServiceIdentifier(type, id));
        }
        
        public void RemoveWithId<T>(long id)
        {
            Remove(new ServiceIdentifier(typeof(T), id));
        }

        public void RemoveTaggedWithId(object service, object tag, long id)
        {
            Remove(new ServiceIdentifier(service.GetType(), tag, id));
        }
        
        public void RemoveTaggedWithId(Type type, object tag, long id)
        {
            Remove(new ServiceIdentifier(type, tag, id));
        }
        
        public void RemoveTaggedWithId<T>(object tag, long id)
        {
            Remove(new ServiceIdentifier(typeof(T), tag, id));
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
        
        public object GetTagged(Type type, object tag)
        {
            return Get(new ServiceIdentifier(type, tag));
        }
        
        public object GetWithId(Type type, long id)
        {
            return Get(new ServiceIdentifier(type, id));
        }
        
        public object GetTaggedWithId(Type type, object tag, long id)
        {
            return Get(new ServiceIdentifier(type, tag, id));
        }
        
        public T GetSingle<T>()
        {
            return (T)Get(new ServiceIdentifier(typeof(T)));
        }
        
        public T GetTagged<T>(object tag)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), tag));
        }
        
        public T GetWithId<T>(long id)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), id));
        }
        
        public T GetTaggedWithId<T>(object tag, long id)
        {
            return (T)Get(new ServiceIdentifier(typeof(T), tag, id));
        }
        
        public bool TryGetSingle(Type serviceType, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType), out service);
        }
        
        public bool TryGetTagged(Type serviceType, object tag, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, tag), out service);
        }
        
        public bool TryGetWithId(Type serviceType, long id, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, id), out service);
        }
        
        public bool TryGetTaggedWithId(Type serviceType, object tag, long id, out object service) 
        {
            return TryGet(new ServiceIdentifier(serviceType, tag, id), out service);
        }

        public bool TryGetSingle<T>(out T service)
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T)), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetTagged<T>(object tag, out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), tag), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetWithId<T>(long id, out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), id), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetTaggedWithId<T>(object tag, long id , out object service) 
        {
            bool result = TryGet(new ServiceIdentifier(typeof(T), tag, id), out var value);
            service = (T)value;
            return result;
        }
        
        public object GetSingleOrDefault(Type type, object defaultValue = default)
        {
            return TryGetSingle(type, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedOrDefault(Type type, object tag, object defaultValue = default)
        {
            return TryGetTagged(type, tag, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetWithIdOrDefault(Type type, long id, object defaultValue = default)
        {
            return TryGetWithId(type, id, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedWithIdOrDefault(Type type, object tag, long id , object defaultValue = default)
        {
            return TryGetTaggedWithId(type, tag, id, out object value) 
                ? value 
                : defaultValue;
        }
        
        public T GetSingleOrDefault<T>(object defaultValue = default)
        {
            return TryGetSingle(typeof(T), out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetTaggedOrDefault<T>(object tag, object defaultValue = default)
        {
            return TryGetTagged(typeof(T), tag, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetWithIdOrDefault<T>(long id, object defaultValue = default)
        {
            return TryGetWithId(typeof(T), id, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetTaggedWithIdOrDefault<T>(object tag, long id , object defaultValue = default)
        {
            return TryGetTaggedWithId(typeof(T), tag, id, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
    }
}