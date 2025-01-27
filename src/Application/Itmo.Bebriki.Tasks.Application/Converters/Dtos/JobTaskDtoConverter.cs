using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Converters.Dtos;

internal static class JobTaskDtoConverter
{
    internal static JobTaskDto ToDto(JobTask jobTask)
    {
        return new JobTaskDto(
            jobTask.Id,
            jobTask.Title,
            jobTask.Description,
            jobTask.AssigneeId,
            jobTask.State,
            jobTask.Priority,
            jobTask.DependOnJobTaskIds,
            jobTask.DeadLine,
            jobTask.UpdatedAt);
    }
}