using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Tasks.Contracts;

public partial class CreateJobTaskRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Title))
        {
            yield return new ValidationResult(
                "Title field is required.",
                [nameof(Title)]);
        }

        if (string.IsNullOrEmpty(Description))
        {
            yield return new ValidationResult(
                "Description field is required.",
                [nameof(Title)]);
        }

        if (AssigneeId <= 0)
        {
            yield return new ValidationResult(
                "Assignee ID is required and must be greater than zero.",
                [nameof(AssigneeId)]);
        }

        if (!Enum.IsDefined(typeof(JobTaskPriority), Priority))
        {
            yield return new ValidationResult(
                "Priority field is invalid.",
                [nameof(Priority)]);
        }

        foreach (long taskId in DependOnTaskIds)
        {
            if (taskId <= 0)
            {
                yield return new ValidationResult(
                    "Job task ID is required and must be greater than zero.",
                    [nameof(taskId)]);
            }
        }

        if (DeadLine is null)
        {
            yield return new ValidationResult(
                "Deadline field is required.",
                [nameof(DeadLine)]);
        }
    }
}