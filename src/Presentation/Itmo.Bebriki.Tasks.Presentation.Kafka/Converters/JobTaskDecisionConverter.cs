using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;
using JobTaskState = Itmo.Bebriki.Tasks.Application.Models.JobTasks.JobTaskState;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;

internal static class JobTaskDecisionConverter
{
    internal static UpdateJobTaskCommand ToInternal(JobTaskDecisionKey key, JobTaskDecisionValue value)
    {
        long? assigneeId = null;
        DateTimeOffset? deadline = null;
        JobTaskState? state = null;

        switch (value.DecisionCase)
        {
            case JobTaskDecisionValue.DecisionOneofCase.JobTaskCreateApprovalResult:
                state = ApprovalResultConverter.ToInternal(value.JobTaskCreateApprovalResult.Result);
                break;
            case JobTaskDecisionValue.DecisionOneofCase.JobTaskUpdateApprovalResult:
                state = ApprovalResultConverter.ToInternal(value.JobTaskCreateApprovalResult.Result);
                assigneeId = value.JobTaskUpdateApprovalResult.ApprovedAssigneeId;
                deadline = value.JobTaskUpdateApprovalResult.ApprovedDeadline?.ToDateTimeOffset();
                break;
        }

        return new UpdateJobTaskCommand(
            JobTaskId: key.JobTaskId,
            AssigneeId: assigneeId,
            State: state,
            DeadLine: deadline);
    }
}