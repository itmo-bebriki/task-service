using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Tasks.Contracts;

public partial class UpdateJobTaskRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (AssigneeId <= 0)
        {
            yield return new ValidationResult(
                "Assignee ID is required and must be greater than zero.",
                [nameof(AssigneeId)]);
        }

        if (!Enum.IsDefined(typeof(JobTaskState), State))
        {
            yield return new ValidationResult(
                "State field is invalid.",
                [nameof(State)]);
        }

        if (!Enum.IsDefined(typeof(JobTaskPriority), Priority))
        {
            yield return new ValidationResult(
                "Priority field is invalid.",
                [nameof(Priority)]);
        }
    }
}