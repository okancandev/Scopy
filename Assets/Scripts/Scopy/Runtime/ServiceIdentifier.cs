using System;

public struct ServiceIdentifier : IEquatable<ServiceIdentifier>
{
    public Type Type;
    public long Id;
    public object Tag;

    public ServiceIdentifier(Type type)
    {
        Type = type;
        Id = 0;
        Tag = null;
    }
    
    public ServiceIdentifier(Type type, long id)
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
    
    public ServiceIdentifier(Type type, long id, object tag)
    {
        Type = type;
        Id = id;
        Tag = tag;
    }
    
    public override string ToString()
    {
        return $"{Type.Name} ({Tag}) ({Id})";
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
        return HashCode.Combine(Type, Id, Tag);
    }

    public static bool operator ==(ServiceIdentifier value1, ServiceIdentifier value2) => value1.Equals(value2);
    public static bool operator !=(ServiceIdentifier value1, ServiceIdentifier value2) => !value1.Equals(value2);
}