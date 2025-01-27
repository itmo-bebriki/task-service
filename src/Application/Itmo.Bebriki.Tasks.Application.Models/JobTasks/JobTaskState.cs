namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public enum JobTaskState
{
    None = 0,
    PendingApproval = 1,
    Approved = 2,
    Rejected = 3,
}