using Arcasharp.Embed;

namespace A
{
    namespace B
    {
        namespace C
        {
            public static partial class NestedNamespaceEmbeddedFile
            {
                [EmbedFile("embedded.txt")]
                public static partial byte[] GetFile();
            }
        }
    }
}