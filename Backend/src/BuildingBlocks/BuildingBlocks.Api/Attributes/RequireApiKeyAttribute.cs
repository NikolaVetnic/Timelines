namespace BuildingBlocks.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class RequireApiKeyAttribute : Attribute { }
