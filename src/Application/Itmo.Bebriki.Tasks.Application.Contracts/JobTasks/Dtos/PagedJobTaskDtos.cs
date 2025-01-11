namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;

public sealed record PagedJobTaskDtos(long? Cursor, IReadOnlyCollection<JobTaskDto> JobTaskDtos);