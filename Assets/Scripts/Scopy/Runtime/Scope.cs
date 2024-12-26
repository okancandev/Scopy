using System;
using System.Collections.Generic;

//id int -> long
//tag before id, for tagged api?

namespace Okancandev.Scopy
{
    public class Scope
    {
        private readonly Dictionary<ServiceIdentifier, object> _services = new();
    
        public IReadOnlyDictionary<ServiceIdentifier, object> Services => _services;

        public void Add(ServiceIdentifier identifier, object service) 
        {
            _services.Add(identifier, service);
        }

        public void AddSingle(object service)
        {
            _services.Add(new ServiceIdentifier(service.GetType()), service);
        }
        
        public void AddWithId(object service, int id)
        {
            _services.Add(new ServiceIdentifier(service.GetType(), id), service);
        }
        
        public void AddTagged(object service, object tag)
        {
            _services.Add(new ServiceIdentifier(service.GetType(), tag), service);
        }

        public void AddTaggedWithId(object service, int id, object tag)
        {
            _services.Add(new ServiceIdentifier(service.GetType(), id, tag), service);
        }
        
        public void RemoveSingle(object service)
        {
            _services.Remove(new ServiceIdentifier(service.GetType()));
        }
        
        public void RemoveWithId(object service, int id)
        {
            _services.Remove(new ServiceIdentifier(service.GetType(), id));
        }
        
        public void RemoveTagged(object service, object tag)
        {
            _services.Remove(new ServiceIdentifier(service.GetType(), tag));
        }

        public void RemoveTaggedWithId(object service, int id, object tag)
        {
            _services.Remove(new ServiceIdentifier(service.GetType(), id, tag));
        }
    
        public bool Remove(ServiceIdentifier identifier) 
        {
            return _services.Remove(identifier);
        }
        
        public object Get(ServiceIdentifier identifier) 
        {
            return _services[identifier];
        }
        
        public object GetSingle(Type type)
        {
            return _services[new ServiceIdentifier(type)];
        }
        
        public object GetWithId(Type type ,int id)
        {
            return _services[new ServiceIdentifier(type, id)];
        }
        
        public object GetTagged(Type type, object tag)
        {
            return _services[new ServiceIdentifier(type, tag)];
        }
        
        public object GetTaggedWithId(Type type, int id, object tag)
        {
            return _services[new ServiceIdentifier(type, id, tag)];
        }
        
        public object GetSingle<T>()
        {
            return _services[new ServiceIdentifier(typeof(T))];
        }
        
        public T GetWithId<T>(int id)
        {
            return (T)_services[new ServiceIdentifier(typeof(T), id)];
        }
        
        public T GetTagged<T>(object tag)
        {
            return (T)_services[new ServiceIdentifier(typeof(T), tag)];
        }
        
        public T GetTaggedWithId<T>(int id, object tag)
        {
            return (T)_services[new ServiceIdentifier(typeof(T), id, tag)];
        }
        
        public bool TryGetSingle(Type serviceType, out object service) 
        {
            return _services.TryGetValue(new ServiceIdentifier(serviceType), out service);
        }
        
        public bool TryGetWithId(Type serviceType, int id, out object service) 
        {
            return _services.TryGetValue(new ServiceIdentifier(serviceType, id), out service);
        }
        
        public bool TryGetTagged(Type serviceType, object tag, out object service) 
        {
            return _services.TryGetValue(new ServiceIdentifier(serviceType, tag), out service);
        }
        
        public bool TryGetTaggedWithId(Type serviceType, int id, object tag, out object service) 
        {
            return _services.TryGetValue(new ServiceIdentifier(serviceType, id, tag), out service);
        }

        public bool TryGetSingle<T>(out T service)
        {
            bool result = _services.TryGetValue(new ServiceIdentifier(typeof(T)), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetWithId<T>(int id, out object service) 
        {
            bool result = _services.TryGetValue(new ServiceIdentifier(typeof(T), id), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetTagged<T>(object tag, out object service) 
        {
            bool result = _services.TryGetValue(new ServiceIdentifier(typeof(T), tag), out var value);
            service = (T)value;
            return result;
        }
        
        public bool TryGetTaggedWithId<T>(int id, object tag, out object service) 
        {
            bool result = _services.TryGetValue(new ServiceIdentifier(typeof(T), id, tag), out var value);
            service = (T)value;
            return result;
        }
        
        public object GetSingleOrDefault(Type type, object defaultValue)
        {
            return TryGetSingle(type, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetWithIdOrDefault(Type type, int id, object defaultValue)
        {
            return TryGetWithId(type, id, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedOrDefault(Type type, object tag, object defaultValue)
        {
            return TryGetTagged(type, tag, out object value) 
                ? value 
                : defaultValue;
        }
        
        public object GetTaggedWithIdOrDefault(Type type, int id, object tag, object defaultValue)
        {
            return TryGetTaggedWithId(type, id, tag, out object value) 
                ? value 
                : defaultValue;
        }
        
        public T GetSingleOrDefault<T>(object defaultValue)
        {
            return TryGetSingle(typeof(T), out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetWithIdOrDefault<T>(int id, object defaultValue)
        {
            return TryGetWithId(typeof(T), id, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetTaggedOrDefault<T>(object tag, object defaultValue)
        {
            return TryGetTagged(typeof(T), tag, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
        
        public T GetTaggedWithIdOrDefault<T>(int id, object tag, object defaultValue)
        {
            return TryGetTaggedWithId(typeof(T), id, tag, out object value) 
                ? (T)value 
                : (T)defaultValue;
        }
    }
}

public struct ServiceIdentifier
{
    public Type Type;
    public int Id;
    public object Tag;

    public ServiceIdentifier(Type type)
    {
        Type = type;
        Id = 0;
        Tag = null;
    }
    
    public ServiceIdentifier(Type type, int id)
    {
        Type = type;
        Id = id;
        Tag = null;
    }
    
    public ServiceIdentifier(Type type, object tag)
    {
        Type = type;
        Id = 0;
        Tag = tag;
    }
    
    public ServiceIdentifier(Type type, int id, object tag)
    {
        Type = type;
        Id = id;
        Tag = tag;
    }

    public override string ToString()
    {
        return $"{Type.Name} ({Tag}) ({Id})";
    }
}