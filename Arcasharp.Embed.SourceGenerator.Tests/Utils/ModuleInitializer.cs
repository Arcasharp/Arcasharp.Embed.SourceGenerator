using System.Runtime.CompilerServices;

namespace Arcasharp.Embed.SourceGenerator.Tests.Utils;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifySourceGenerators.Enable();
    }
}