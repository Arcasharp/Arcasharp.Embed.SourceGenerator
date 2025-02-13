namespace Arcasharp.Embed.IntegrationTests;

public class EmbedFileAsStringIntegrationTests
{
    [Fact]
    public void File_content_can_be_read_as_string_from_method_with_EmbedFile_attribute()
    {
        string expectedContent = "This is an embedded file 😁";
        string sut = EmbeddedFiles.GetFileString();

        Assert.Equal(expectedContent, sut);
    }

    [Fact]
    public void Class_can_embed_a_large_file_as_string()
    {
        string expectedContent = File.ReadAllText("octocat.jpg");
        string sut = EmbeddedFiles.GetOctocatString();

        Assert.Equal(expectedContent, sut);
    }
}