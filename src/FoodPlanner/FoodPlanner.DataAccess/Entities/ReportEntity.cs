using System.ComponentModel.DataAnnotations;

namespace FoodPlanner.DataAccess.Entities;

public sealed class ReportEntity
{
    [Key]
    public Guid AttachmentId { get; set; }
    public byte[] ReportContent { get; set; } = Array.Empty<byte>();
}
