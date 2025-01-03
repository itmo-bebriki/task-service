using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;

public static class JobTaskDtoConverter
{
    public static JobTaskDto ToDto(JobTask jobTask)
    {
        return new JobTaskDto(
            jobTask.Id,
            jobTask.Title,
            jobTask.Description,
            jobTask.AssigneeId,
            jobTask.State,
            jobTask.Priority,
            (IReadOnlyList<long>)jobTask.DependOnTasks,
            jobTask.DeadLine,
            jobTask.UpdatedAt);
    }

    /*
    public static JobTask ToDomain(this JobTaskDto dto)
    {
        return new JobTask(
            dto.Id,
            dto.Title,
            dto.Description,
            dto.AssigneeId,
            dto.State,
            dto.Priority,
            dto.DependOnTasks,
            dto.DeadLine,
            dto.UpdatedAt);
    }*/
}