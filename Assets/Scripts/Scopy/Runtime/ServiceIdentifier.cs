using System;

public struct ServiceIdentifier : IEquatable<ServiceIdentifier>
{
    public Type Type;
    public object Tag;
    public long Id;

    public ServiceIdentifier(Type type)
    {
        Type = type;
        Tag = null;
        Id = 0;
    }
    
    public ServiceIdentifier(Type type, long id)
    {
        Type = type;
        Tag = null;
        Id = id;
    }
    
    public ServiceIdentifier(Type type, object tag)
    {
        Type = type;
        Tag = tag;
        Id = 0;
    }
    
    public ServiceIdentifier(Type type, object tag, long id)
    {
        Type = type;
        Tag = tag;
        Id = id;
    }
    
    public override string ToString()
    {
        string tag = Tag != null ? $"t:{Tag}" : "";
        string id = Id > 0 ? $"({Id})" : "";
        return $"{Type.Name} {tag} {id}";
    }

    public bool Equals(ServiceIdentifier other)
    {
        return Equals(Type, other.Type) && Id == other.Id && Equals(Tag, other.Tag);
    }

    public override bool Equals(object obj)
    {
        return obj is ServiceIdentifier other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Tag, Id);
    }

    public static bool operator ==(ServiceIdentifier value1, ServiceIdentifier value2) => value1.Equals(value2);
    public static bool operator !=(ServiceIdentifier value1, ServiceIdentifier value2) => !value1.Equals(value2);
}