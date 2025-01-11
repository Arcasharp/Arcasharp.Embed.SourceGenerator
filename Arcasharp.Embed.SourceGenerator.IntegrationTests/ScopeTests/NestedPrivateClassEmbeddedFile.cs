namespace Arcasharp.Embed.IntegrationTests.ScopeTests;

public static partial class NestedPrivateClassEmbeddedFile
{
    public static byte[] GetFile()
    {
        return NestedPrivateClass.GetEmbeddedFile();
    }

    private static partial class NestedPrivateClass
    {
        [EmbedFile("embedded.txt")]
        public static partial byte[] GetEmbeddedFile();
    }
}