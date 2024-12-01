using Arcasharp.Embed.SourceGenerator.Tests.Utils;

namespace Arcasharp.Embed.SourceGenerator.Tests.Tests;

[UsesVerify]
public class EmbedFileGeneratorTest
{
    [Fact]
    public Task A_file_can_be_embedded()
    {
        string source = """
                           using Arcasharp.Embed;
                           
                           namespace Hello {
                               public static partial class EmbeddedFiles
                               {
                                   [EmbedFile("file.txt")]
                                   public static partial byte[] GetFile();
                               }
                            }
                        """;

        return TestHelper.Verify(source);
    }

    [Fact]
    public Task A_file_can_be_embedded_in_an_internal_class()
    {
        string source = """
                           using Arcasharp.Embed;
                           
                           namespace Hello {
                               internal static partial class EmbeddedFiles
                               {
                                   [EmbedFile("file.txt")]
                                   public static partial byte[] GetFile();
                               }
                            }
                        """;

        return TestHelper.Verify(source);
    }

    [Fact]
    public Task A_file_with_private_scope_can_be_embedded_in_an_internal_class()
    {
        string source = """
                           using Arcasharp.Embed;
                           
                           namespace Hello {
                               internal static partial class EmbeddedFiles
                               {
                                   [EmbedFile("file.txt")]
                                   private static partial byte[] GetFile();
                               }
                            }
                        """;

        return TestHelper.Verify(source);
    }

    [Fact]
    public Task Multiple_files_can_be_embedded_in_the_same_class()
    {
        string source = """
                           using Arcasharp.Embed;
                           
                           namespace Hello {
                               internal static partial class EmbeddedFiles
                               {
                                    [EmbedFile("file.txt")]
                                    private static partial byte[] GetFile();
                                    [EmbedFile("second-file.txt")]
                                    private static partial byte[] GetSecondFile();
                               }
                            }
                        """;

        return TestHelper.Verify(source);
    }

    [Fact]
    public Task A_file_that_does_not_exist_shows_an_error()
    {
        string source = """
                           using Arcasharp.Embed;
                           
                           namespace Hello {
                               public static partial class EmbeddedFiles
                               {
                                   [EmbedFile("file-that-does-not-exist.txt")]
                                   public static partial byte[] GetFile();
                               }
                            }
                        """;

        return TestHelper.Verify(source);
    }
}