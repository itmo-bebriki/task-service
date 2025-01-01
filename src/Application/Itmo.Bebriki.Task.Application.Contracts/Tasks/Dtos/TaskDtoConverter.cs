namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Dtos;

public static class TaskDtoConverter
{
    public static TaskDto ToDto(this Models.Tasks.Task task)
    {
        return new TaskDto(
            task.Id,
            task.Title,
            task.Description,
            task.AssigneeId,
            task.State,
            task.Priority,
            task.DependOnTasks,
            task.DeadLine,
            task.UpdatedAt
        );
    }

    public static Models.Tasks.Task ToDomain(this TaskDto dto)
    {
        return new Models.Tasks.Task(
            dto.Id,
            dto.Title,
            dto.Description,
            dto.AssigneeId,
            dto.State,
            dto.Priority,
            dto.DependOnTasks,
            dto.DeadLine,
            // TODO
            DateTimeOffset.Now);
    }
}