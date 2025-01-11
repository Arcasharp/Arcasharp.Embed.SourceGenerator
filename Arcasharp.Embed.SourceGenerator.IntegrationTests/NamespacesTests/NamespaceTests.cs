using A.B.C;

namespace Arcasharp.Embed.IntegrationTests.NamespacesTests;

public class NamespaceTests
{
    [Fact]
    public void File_content_can_be_read_from_class_in_file_scope_namespace()
    {
        byte[] expectedContent = "This is the content of the embbeded file 🚀"u8.ToArray();
        byte[] sut = FileScopedNamespaceEmbeddedFile.GetFile();

        Assert.Equal(expectedContent, sut);
    }
    
    [Fact]
    public void File_content_can_be_read_from_class_in_block_scope_namespace()
    {
        byte[] expectedContent = "This is the content of the embbeded file 🚀"u8.ToArray();
        byte[] sut = BlockScopedNamespaceEmbeddedFile.GetFile();

        Assert.Equal(expectedContent, sut);
    }
    
    [Fact]
    public void File_content_can_be_read_from_class_in_global_namespace()
    {
        byte[] expectedContent = "This is the content of the embbeded file 🚀"u8.ToArray();
        byte[] sut = GlobalNamespaceEmbeddedFile.GetFile();

        Assert.Equal(expectedContent, sut);
    }
    
    [Fact]
    public void File_content_can_be_read_from_class_in_nested_namespace()
    {
        byte[] expectedContent = "This is the content of the embbeded file 🚀"u8.ToArray();
        byte[] sut = NestedNamespaceEmbeddedFile.GetFile();

        Assert.Equal(expectedContent, sut);
    }
}