using Microsoft.CodeAnalysis;

namespace Arcasharp.Embed.SourceGenerator;

internal sealed class EmbeddedFile
{
    public string FileName => Path.GetFileName(FullFilePath);
    public required string MethodName { get; init; }
    public required string FullFilePath { get; init; }
    public required string Namespace { get; init; }
    public required string ClassName { get; init; }
    public required SyntaxNode Node { get; init; }
    public required string ClassVisibility { get; init; }
    public required string MethodVisibility { get; init; }
}