using ChatConnectData.Interfaces;

namespace ChatConnectData.Models;

public class BaseEntity<T> : IEntity<T>, IAuditable 
{
    public T? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public T GetId()
    {
        return Id!;
    }
}