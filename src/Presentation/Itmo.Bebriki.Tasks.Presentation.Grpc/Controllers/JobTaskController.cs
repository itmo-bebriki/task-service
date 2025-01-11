using Grpc.Core;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;
using Itmo.Bebriki.Tasks.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Requests;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Responses;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Controllers;

public sealed class JobTaskController : JobTaskService.JobTaskServiceBase
{
    private readonly IJobTaskService _jobTaskService;

    public JobTaskController(IJobTaskService jobTaskService)
    {
        _jobTaskService = jobTaskService;
    }

    public override async Task<QueryJobTaskResponse> QueryJobTasks(
        QueryJobTaskRequest request,
        ServerCallContext context)
    {
        QueryJobTaskCommand internalCommand = QueryJobTaskRequestConverter.ToInternal(request);

        PagedJobTaskDtos internalResponse =
            await _jobTaskService.QueryJobTasksAsync(internalCommand, context.CancellationToken);

        QueryJobTaskResponse response = QueryJobTaskResponseConverter.FromInternal(internalResponse);

        return response;
    }

    public override async Task<CreateJobTaskResponse> CreateJobTask(
        CreateJobTaskRequest request,
        ServerCallContext context)
    {
        CreateJobTaskCommand internalCommand = CreateJobTaskRequestConverter.ToInternal(request);

        long internalResponse = await _jobTaskService.CreateJobTaskAsync(internalCommand, context.CancellationToken);

        CreateJobTaskResponse response = CreateJobTaskResponseConverter.FromInternal(internalResponse);

        return response;
    }

    public override async Task<UpdateJobTaskResponse> UpdateJobTask(
        UpdateJobTaskRequest request,
        ServerCallContext context)
    {
        UpdateJobTaskCommand internalCommand = UpdateJobTaskRequestConverter.ToInternal(request);

        await _jobTaskService.UpdateJobTaskAsync(internalCommand, context.CancellationToken);

        return new UpdateJobTaskResponse();
    }

    public override async Task<AddJobTaskDependenciesResponse> AddJobTaskDependencies(
        SetJobTaskDependenciesRequest request,
        ServerCallContext context)
    {
        SetJobTaskDependenciesCommand internalCommand = SetJobTaskDependenciesRequestConverter.ToInternal(request);

        await _jobTaskService.AddJobTaskDependenciesAsync(internalCommand, context.CancellationToken);

        return new AddJobTaskDependenciesResponse();
    }

    public override async Task<RemoveJobTaskDependenciesResponse> RemoveJobTaskDependencies(
        SetJobTaskDependenciesRequest request,
        ServerCallContext context)
    {
        SetJobTaskDependenciesCommand internalCommand = SetJobTaskDependenciesRequestConverter.ToInternal(request);

        await _jobTaskService.RemoveJobTaskDependenciesAsync(internalCommand, context.CancellationToken);

        return new RemoveJobTaskDependenciesResponse();
    }
}