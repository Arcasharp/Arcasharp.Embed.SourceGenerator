namespace Arcasharp.Embed.IntegrationTests.ScopeTests;

public static partial class EmbeddedPrivateFile
{
    [EmbedFile("embedded.txt")]
    private static partial byte[] GetPrivateFile();

    public static byte[] GetFile()
    {
        return GetPrivateFile();
    }
}