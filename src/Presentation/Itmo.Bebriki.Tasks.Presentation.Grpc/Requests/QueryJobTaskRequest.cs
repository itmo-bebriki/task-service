using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Tasks.Contracts;

public partial class QueryJobTaskRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (JobTaskIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "JobTaskIds contains invalid values. Job task ID is required and must be greater than zero.",
                [nameof(JobTaskIds)]);
        }

        if (AssigneeIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "AssigneeIds contains invalid values. Assignee ID is required and must be greater than zero.",
                [nameof(AssigneeIds)]);
        }

        if (States.Any(state => !Enum.IsDefined(typeof(JobTaskState), state)))
        {
            yield return new ValidationResult(
                "States contains invalid JobTaskState values.",
                [nameof(States)]);
        }

        if (Priorities.Any(priority => !Enum.IsDefined(typeof(JobTaskPriority), priority)))
        {
            yield return new ValidationResult(
                "Priorities contains invalid JobTaskPriority values.",
                [nameof(Priorities)]);
        }

        if (Cursor < 0)
        {
            yield return new ValidationResult(
                "Cursor must be greater than or equal to 0.",
                [nameof(Cursor)]);
        }

        if (PageSize < 0)
        {
            yield return new ValidationResult(
                "PageSize must be greater than or equal to 0.",
                [nameof(PageSize)]);
        }
    }
}