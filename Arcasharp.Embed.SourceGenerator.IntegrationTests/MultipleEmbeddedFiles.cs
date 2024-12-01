namespace Arcasharp.Embed.IntegrationTests;

public static partial class MultipleEmbeddedFiles
{
    [EmbedFile("embedded.txt")]
    public static partial byte[] GetFile();

    [EmbedFile("embedded.txt")]
    public static partial byte[] GetFileASecondTime();

    [EmbedFile("other-embedded.txt")]
    public static partial byte[] GetSecondFile();
}