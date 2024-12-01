using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Arcasharp.Embed.SourceGenerator;

internal static class SyntaxValueProviderExtensions
{
    public static IncrementalValuesProvider<EmbeddedFile> CreateEmbeddedFileSyntaxValueProvider(
        this IncrementalGeneratorInitializationContext provider)
    {
        return provider.SyntaxProvider.CreateSyntaxProvider(
            CheckEmbeddedFileSyntax,
            TransformEmbeddedFileSyntax
        );
    }

    private static EmbeddedFile TransformEmbeddedFileSyntax(GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        MethodDeclarationSyntax method = (MethodDeclarationSyntax)context.Node;
        AttributeSyntax? attribute = method.AttributeLists.SelectMany(x => x.Attributes).First(IsValidAttribute);
        string filePath = GetArgumentValue(attribute, 0)!.Replace("\"", string.Empty);
        string sourceFilePath = context.SemanticModel.SyntaxTree.FilePath;
        string directory = Path.GetDirectoryName(sourceFilePath)!;
        string fullFilePath = Path.Combine(directory, filePath);
        string methodName = method.Identifier.Text;
        string namespaceName = context.SemanticModel.GetDeclaredSymbol(method)?.ContainingNamespace?.ToString() ??
                               string.Empty;
        ClassDeclarationSyntax? parentClass = method.Parent as ClassDeclarationSyntax;
        string className = parentClass?.Identifier.Text ?? string.Empty;
        string classVisibility = GetVisibility(parentClass);
        string methodVisibility = GetVisibility(method);
        return new EmbeddedFile
        {
            MethodName = methodName,
            FullFilePath = fullFilePath,
            Namespace = namespaceName,
            ClassName = className,
            Node = attribute,
            ClassVisibility = classVisibility,
            MethodVisibility = methodVisibility
        };
    }

    private static string GetVisibility(SyntaxNode node)
    {
        SyntaxTokenList modifiers = node switch
        {
            ClassDeclarationSyntax classDeclaration => classDeclaration.Modifiers,
            MethodDeclarationSyntax methodDeclaration => methodDeclaration.Modifiers,
            _ => new SyntaxTokenList()
        };

        if (modifiers.Any(SyntaxKind.PublicKeyword))
        {
            return "public";
        }

        if (modifiers.Any(SyntaxKind.ProtectedKeyword))
        {
            return "protected";
        }

        if (modifiers.Any(SyntaxKind.InternalKeyword))
        {
            return "internal";
        }

        if (modifiers.Any(SyntaxKind.PrivateKeyword))
        {
            return "private";
        }

        return string.Empty;
    }

    private static string? GetArgumentValue(AttributeSyntax attribute, int argumentIndex)
    {
        AttributeArgumentSyntax? argument =
            attribute.ArgumentList?.Arguments[argumentIndex];
        return argument?.Expression.ToString();
    }

    private static bool CheckEmbeddedFileSyntax(SyntaxNode node, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        MethodDeclarationSyntax? methodDeclaration = node as MethodDeclarationSyntax;
        if (methodDeclaration == null)
        {
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();
        bool isPartial = methodDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword);
        if (!isPartial)
        {
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();
        AttributeSyntax[] attributes = methodDeclaration.AttributeLists.SelectMany(x => x.Attributes).ToArray();

        if (attributes.Length == 0)
        {
            return false;
        }

        return Array.Exists(attributes, IsValidAttribute);
    }

    public static bool IsValidAttribute(AttributeSyntax attribute)
    {
        return attribute.Name.ToString() == EmbedFileAttributeSourceGeneratorOutput.AttributeName ||
               attribute.Name.ToString() ==
               EmbedFileAttributeSourceGeneratorOutput.AttributeName.Replace("Attribute", "");
    }
}