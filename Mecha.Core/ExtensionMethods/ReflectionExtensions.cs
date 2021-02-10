using System;
using System.Text;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Reflection extension
    /// </summary>
    internal static class ReflectionExtensions
    {
        /// <summary>
        /// Returns the type's name (Actual C# name, not the funky version from the Name property)
        /// </summary>
        /// <param name="objectType">Type to get the name of</param>
        /// <returns>string name of the type</returns>
        public static string GetName(this Type objectType)
        {
            if (objectType is null)
            {
                return string.Empty;
            }

            if (objectType.Name == "Void")
            {
                return "void";
            }
            var Output = new StringBuilder();
            Output.Append(objectType.DeclaringType is null ? objectType.Namespace : objectType.DeclaringType.GetName())
                .Append('.');
            if (objectType.Name.Contains("`", StringComparison.Ordinal))
            {
                var GenericTypes = objectType.GetGenericArguments();
                Output
                    .Append(objectType.Name, 0, objectType.Name.IndexOf("`", StringComparison.Ordinal))
                    .Append('<');
                var Seperator = "";
                for (int x = 0, GenericTypesLength = GenericTypes.Length; x < GenericTypesLength; x++)
                {
                    var GenericType = GenericTypes[x];
                    Output.Append(Seperator).Append(GenericType.GetName());
                    Seperator = ",";
                }

                Output.Append('>');
            }
            else
            {
                Output.Append(objectType.Name);
            }
            return Output.ToString().Replace("&", "", StringComparison.Ordinal);
        }
    }
}