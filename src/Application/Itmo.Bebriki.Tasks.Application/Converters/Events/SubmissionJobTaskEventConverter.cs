using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class SubmissionJobTaskEventConverter
{
    internal static SubmissionJobTaskEvent ToEvent(UpdateJobTaskContext context)
    {
        return new SubmissionJobTaskEvent(
            JobTaskId: context.JobTaskId,
            AssigneeId: context.AssigneeId,
            DeadLine: context.DeadLine);
    }
}