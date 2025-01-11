using Microsoft.CodeAnalysis;

namespace Arcasharp.Embed.SourceGenerator;

internal sealed class EmbeddedFile
{
    public string FileName => Path.GetFileName(FullFilePath);
    public required string MethodName { get; init; }
    public required string FullFilePath { get; init; }
    public required string Namespace { get; init; }
    public required ClassDefinition Class { get; init; }
    public required SyntaxNode Node { get; init; }
    public required string MethodVisibility { get; init; }
    public required DateTimeOffset? LastUpdated { get; set; }
}

internal sealed class ClassDefinition
{
    public required string Name { get; init; }
    public required string Visibility { get; init; }
    public ClassDefinition? Parent { get; init; }
}