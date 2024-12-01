namespace Arcasharp.Embed.IntegrationTests;

public class EmbedFileIntegrationTests
{
    [Fact]
    public void File_content_can_be_read_from_method_with_EmbedFile_attribute()
    {
        byte[] expectedContent = "This is an embedded file 😁"u8.ToArray();
        byte[] sut = EmbeddedFiles.GetFile();

        Assert.Equal(expectedContent, sut);
    }

    [Fact]
    public void File_content_can_be_read_from_private_method_with_EmbedFile_attribute()
    {
        byte[] expectedContent = "This is an embedded file 😁"u8.ToArray();
        byte[] sut = EmbeddedPrivateFile.GetFile();

        Assert.Equal(expectedContent, sut);
    }

    [Fact]
    public void File_content_can_be_read_from_internal_method_with_EmbedFile_attribute()
    {
        byte[] expectedContent = "This is an embedded file 😁"u8.ToArray();
        byte[] sut = EmbeddedInternalFile.GetFile();

        Assert.Equal(expectedContent, sut);
    }

    [Fact]
    public void Class_can_embed_multiple_files()
    {
        byte[] expectedContent = "This is an embedded file 😁"u8.ToArray();
        byte[] sut = MultipleEmbeddedFiles.GetFile();

        Assert.Equal(expectedContent, sut);
        Assert.Equal(expectedContent, MultipleEmbeddedFiles.GetFileASecondTime());
        Assert.NotEqual(MultipleEmbeddedFiles.GetSecondFile(), MultipleEmbeddedFiles.GetFileASecondTime());
    }
}