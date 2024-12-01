# Arcasharp.Embed.SourceGenerator

Arcasharp.Embed.SourceGenerator is a C# source generator that allows you to embed files directly into your C# code. This can be useful for including static resources such as configuration files, images, or other assets directly within your assemblies.

## Getting Started

To get started with Arcasharp.Embed.SourceGenerator, follow these steps:

### Installation

1. Add the `Arcasharp.Embed.SourceGenerator` package to your project. You can do this via the NuGet Package Manager or by adding the following line to your `.csproj` file:

    ```xml
    <PackageReference Include="Arcasharp.Embed.SourceGenerator" Version="1.0.0" />
    ```

2. Restore the NuGet packages:

    ```sh
    dotnet restore
    ```

### Usage

To embed a file into your project, use the `[EmbedFile]` attribute on a partial method. The source generator will automatically generate the necessary code to include the file as a byte array.

#### Example 1: Embedding a file

```csharp
using Arcasharp.Embed;

namespace MyNamespace
{
    public static partial class EmbeddedFiles
    {
        [EmbedFile("path/to/your/file.txt")]
        public static partial byte[] GetFile();
    }
}