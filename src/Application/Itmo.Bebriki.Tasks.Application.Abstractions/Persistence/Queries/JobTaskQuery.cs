using SourceKit.Generators.Builder.Annotations;

namespace Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public sealed partial record JobTaskQuery(long[] TaskIds, [RequiredValue] int PageSize, long Cursor);