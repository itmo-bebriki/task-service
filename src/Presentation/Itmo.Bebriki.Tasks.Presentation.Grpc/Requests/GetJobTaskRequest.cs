using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Tasks.Contracts;

public partial class GetJobTaskRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (JobTaskId <= 0)
        {
            yield return new ValidationResult(
                "Job task ID is required and must be greater than zero.",
                [nameof(JobTaskId)]);
        }
    }
}