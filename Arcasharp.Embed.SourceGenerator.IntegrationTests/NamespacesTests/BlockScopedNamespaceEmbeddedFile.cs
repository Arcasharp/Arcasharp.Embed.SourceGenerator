namespace Arcasharp.Embed.IntegrationTests.NamespacesTests
{
    public static partial class BlockScopedNamespaceEmbeddedFile
    {
        [EmbedFile("embedded.txt")]
        public static partial byte[] GetFile();
    }
}