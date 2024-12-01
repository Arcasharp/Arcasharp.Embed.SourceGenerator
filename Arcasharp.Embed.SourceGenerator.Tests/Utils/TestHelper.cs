using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Arcasharp.Embed.SourceGenerator.Tests.Utils;

public static class TestHelper
{
    public static Task Verify(string source, [CallerFilePath] string? path = null)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source, path: path ?? string.Empty);

        // Create a Roslyn compilation for the syntax tree.
        CSharpCompilation compilation = CSharpCompilation.Create(
            "Tests",
            new[] { syntaxTree });

        // Create an instance of our EnumGenerator incremental source generator
        EmbedFileSourceGenerator generator = new();

        // The GeneratorDriver is used to run our generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the source generator!
        driver = driver.RunGenerators(compilation);

        // Use verify to snapshot test the source generator output!
        return Verifier
            .Verify(driver)
            .UseDirectory("Snapshots");
    }
}