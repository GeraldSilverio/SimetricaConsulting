using TaskManagement.Core.Domain.Commons;

namespace TaskManagement.Core.Domain.Entities;

public class TaskStatus : AuditoryProperties
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Tasks> Tasks { get; set; }
}