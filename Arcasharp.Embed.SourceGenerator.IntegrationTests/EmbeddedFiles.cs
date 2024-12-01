namespace Arcasharp.Embed.IntegrationTests;

public static partial class EmbeddedFiles
{
    [EmbedFile("embedded.txt")]
    public static partial byte[] GetFile();
}