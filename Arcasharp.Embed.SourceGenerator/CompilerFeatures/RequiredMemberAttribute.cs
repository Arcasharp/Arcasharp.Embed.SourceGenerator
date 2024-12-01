// ReSharper disable once CheckNamespace

namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter |
                AttributeTargets.ReturnValue)]
public sealed class RequiredMemberAttribute : Attribute
{
    public RequiredMemberAttribute()
    {
    }

    public RequiredMemberAttribute(string message)
    {
        Message = message;
    }


    public string? Message { get; }
}