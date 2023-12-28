namespace Mecha.Core.Generator
{
    /// <summary>
    /// Parameter data class
    /// </summary>
    /// <param name="CreatedBy">Created by</param>
    /// <param name="Value">Value</param>
    public record ParameterValue(string CreatedBy, object? Value);
}