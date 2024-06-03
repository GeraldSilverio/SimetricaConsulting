using TaskManagement.Core.Domain.Commons;

namespace TaskManagement.Core.Domain.Entities;

public class Tasks : AuditoryProperties
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IdUser { get; set; }
    public int IdTaskStatus { get; set; }
    public TaskStatus TaskStatus { get; set; }
}