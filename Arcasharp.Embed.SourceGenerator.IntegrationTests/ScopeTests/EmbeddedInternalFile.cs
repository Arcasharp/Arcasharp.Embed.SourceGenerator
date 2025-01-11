namespace Arcasharp.Embed.IntegrationTests.ScopeTests;

public static partial class EmbeddedInternalFile
{
    [EmbedFile("embedded.txt")]
    internal static partial byte[] GetPrivateFile();

    public static byte[] GetFile()
    {
        return GetPrivateFile();
    }
}