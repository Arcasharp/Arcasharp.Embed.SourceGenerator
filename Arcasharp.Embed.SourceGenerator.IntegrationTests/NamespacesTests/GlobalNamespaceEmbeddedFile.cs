using Arcasharp.Embed;

#pragma warning disable CA1050
#pragma warning disable S3903
public static partial class GlobalNamespaceEmbeddedFile
#pragma warning restore S3903
#pragma warning restore CA1050
{
    [EmbedFile("embedded.txt")]
    public static partial byte[] GetFile();
}