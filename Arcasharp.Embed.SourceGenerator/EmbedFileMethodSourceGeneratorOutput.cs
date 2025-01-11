using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Arcasharp.Embed.SourceGenerator;

internal sealed class EmbedFileMethodSourceGeneratorOutput
{
    private readonly EmbeddedFile _file;

    public EmbedFileMethodSourceGeneratorOutput(EmbeddedFile file)
    {
        _file = file;
    }

    public string GenerateSource()
    {
        string contentAsByteArrayInitializer = GetContentAsByteArrayInitializer(_file);

        string codeWithoutNamespace = $$"""
                                        // <auto-generated>
                                        #nullable enable

                                        using global::System.Runtime.CompilerServices;

                                        {{GetClassBeginBlock(_file)}}
                                            
                                            [CompilerGenerated]
                                            private static byte[] _{{_file.MethodName}} = {{contentAsByteArrayInitializer}};
                                          
                                            [CompilerGenerated]
                                            {{_file.MethodVisibility}} static partial byte[] {{_file.MethodName}}() 
                                            {
                                                return _{{_file.MethodName}};
                                            }
                                            
                                        {{GetClassEndBlock(_file)}}
                                        """;

        string codeWithNamespace = $$"""
                                     // <auto-generated>
                                     #nullable enable

                                     using global::System.Runtime.CompilerServices;

                                     namespace {{_file.Namespace}}
                                     {
                                         {{GetClassBeginBlock(_file)}}
                                         
                                             [CompilerGenerated]
                                             private static byte[] _{{_file.MethodName}} = {{contentAsByteArrayInitializer}};
                                           
                                             [CompilerGenerated]
                                             {{_file.MethodVisibility}} static partial byte[] {{_file.MethodName}}() 
                                             {
                                                 return _{{_file.MethodName}};
                                             }
                                             
                                         {{GetClassEndBlock(_file)}}
                                     }
                                     """;

        string code = _file.Namespace == string.Empty ? codeWithoutNamespace : codeWithNamespace;
        return CSharpSyntaxTree
            .ParseText(code)
            .GetRoot()
            .NormalizeWhitespace()
            .ToFullString();
    }

    private static string GetClassBeginBlock(EmbeddedFile file)
    {
        StringBuilder builder = new();
        List<ClassDefinition> classes = new();
        ClassDefinition? currentClass = file.Class;
        while (currentClass is not null)
        {
            classes.Add(currentClass);
            currentClass = currentClass.Parent;
        }

        classes.Reverse();

        foreach (ClassDefinition? @class in classes)
        {
            builder.AppendLine($"{@class.Visibility} static partial class {@class.Name}");
            builder.AppendLine("{");
        }

        return builder.ToString();
    }

    public static string GetClassEndBlock(EmbeddedFile file)
    {
        StringBuilder builder = new();
        List<ClassDefinition> classes = new();
        ClassDefinition? currentClass = file.Class;
        while (currentClass is not null)
        {
            classes.Add(currentClass);
            currentClass = currentClass.Parent;
        }

        int classCount = classes.Count;
        for (int i = 0; i < classCount; i++)
        {
            builder.AppendLine("}");
        }

        return builder.ToString();
    }

    private static string GetContentAsByteArrayInitializer(EmbeddedFile file)
    {
        StringBuilder builder = new();
        builder.Append("new byte[] { ");
        byte[] content = File.ReadAllBytes(file.FullFilePath);
        foreach (byte b in content)
        {
            builder.AppendFormat("0x{0:X2}, ", b);
        }

        builder.Remove(builder.Length - 2, 2);
        builder.Append(" }");
        return builder.ToString();
    }
}