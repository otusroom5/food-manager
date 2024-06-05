namespace FoodUserAuth.BusinessLogic.Dto;

public sealed class ApiKeyDto : IEquatable<ApiKeyDto>, ICloneable
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public DateTime ExpiryDate { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public bool Equals(ApiKeyDto other)
    {
        return other is not null
             && Id.Equals(other.Id)
             && Key == other.Key
             && ExpiryDate == other.ExpiryDate;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ApiKeyDto);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Key, ExpiryDate);
    }
}
