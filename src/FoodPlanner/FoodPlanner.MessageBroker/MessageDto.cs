namespace FoodPlanner.MessageBroker;

public class MessageDto
{
    public Guid Id { get; set; }
    public string Group { get; set; }
    public string Message { get; set; }
    public List<Guid> AttachmentIds { get; set; } = new List<Guid>();
}
