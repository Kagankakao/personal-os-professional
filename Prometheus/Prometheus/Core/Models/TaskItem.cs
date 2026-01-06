using System;

namespace KeganOS.Core.Models;

public enum TaskType
{
    Daily,
    LongTerm
}

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public TaskType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
