using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Tasks.Contracts;

public partial class SetJobTaskDependenciesRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (JobTaskId <= 0)
        {
            yield return new ValidationResult(
                "Job task ID is required and must be greater than zero.",
                [nameof(JobTaskId)]);
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
    }
}