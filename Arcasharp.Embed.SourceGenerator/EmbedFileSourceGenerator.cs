using Microsoft.CodeAnalysis;

namespace Arcasharp.Embed.SourceGenerator;

[Generator]
public sealed class EmbedFileSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(EmbedFileAttributeSourceGeneratorOutput.AddAttribute);
        IncrementalValuesProvider<EmbeddedFile> provider = context.CreateEmbeddedFileSyntaxValueProvider();

        context.RegisterSourceOutput(provider, (ctx, file) =>
        {
            if (!File.Exists(file.FullFilePath))
            {
                DiagnosticDescriptor diagnostic = new(
                    "ASG0001",
                    $"The file '{file.FileName}' does not exist",
                    $"The file '{file.FullFilePath}' does not exist",
                    "Arcasharp.Embed.SourceGenerator",
                    DiagnosticSeverity.Error,
                    true
                );

                ctx.ReportDiagnostic(Diagnostic.Create(diagnostic, file.Node.GetLocation()));

                return;
            }

            if (file.MethodType is null)
            {
                DiagnosticDescriptor diagnostic = new(
                    "ASG0002",
                    $"The method return type is invalid",
                    $"The method {file.MethodName} must return either a byte[] or a string",
                    "Arcasharp.Embed.SourceGenerator",
                    DiagnosticSeverity.Error,
                    true
                );

                ctx.ReportDiagnostic(Diagnostic.Create(diagnostic, file.Node.GetLocation()));
                return;
            }

            EmbedFileMethodSourceGeneratorOutput output = new(file);
            ctx.AddSource($"{file.Class.Name}.{file.MethodName}.g.cs", output.GenerateSource());
        });
    }
}