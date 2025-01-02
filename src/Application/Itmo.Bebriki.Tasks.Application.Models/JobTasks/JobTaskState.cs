namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public enum JobTaskState
{
    None = 0,
    Backlog = 1,
    ToDo = 2,
    InProgress = 3,
    InReview = 4,
    Done = 5,
    Closed = 6,
}