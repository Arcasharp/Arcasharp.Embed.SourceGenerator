﻿//HintName: EmbedFileAttribute.g.cs
// <auto-generated>
#nullable enable

using global::System;
using global::System.Runtime.CompilerServices;

namespace Arcasharp.Embed {
    
    /// <summary>
    /// Embeds a file into the assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [CompilerGenerated]
    public sealed class EmbedFileAttribute : Attribute
    {
        /// <summary>
        /// Create an instance of EmbedFileAttribute
        /// </summary>
        /// <param name="Name">The absolute or relative path to the file to embed (relative to the project root path)</param>
        public EmbedFileAttribute(string Name)
        {
        
        }
    }
}