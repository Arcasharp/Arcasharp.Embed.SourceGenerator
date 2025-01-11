namespace Arcasharp.Embed.IntegrationTests.NamespacesTests;

public static partial class FileScopedNamespaceEmbeddedFile
{
    [EmbedFile("embedded.txt")]
    public static partial byte[] GetFile();
}