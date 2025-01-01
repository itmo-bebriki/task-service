namespace Itmo.Bebriki.Task.Application.Models.Tasks;

public enum TaskState
{
    None = 0,
    Backlog = 1,
    ToDo = 2,
    InProgress = 3,
    InReview = 4,
    Done = 5,
    Closed = 6,
}