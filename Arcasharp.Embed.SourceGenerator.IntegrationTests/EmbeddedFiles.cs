namespace Arcasharp.Embed.IntegrationTests;

public static partial class EmbeddedFiles
{
    [EmbedFile("embedded.txt")]
    public static partial byte[] GetFile();
    
    [EmbedFile("embedded.txt")]
    public static partial string GetFileString();

    [EmbedFile("octocat.jpg")]
    public static partial byte[] GetOctocat();

    [EmbedFile("octocat.jpg")]
    public static partial string GetOctocatString();
}