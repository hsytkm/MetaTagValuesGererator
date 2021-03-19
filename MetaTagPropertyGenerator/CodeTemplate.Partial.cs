#pragma warning disable IDE0057
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace MyGenerator.MetaTagProperty
{
    public partial class CodeTemplate
    {
        internal string Namespace { get; set; } = "";
        internal string ClassName { get; }
        internal IReadOnlyList<AttributeFieldSource> AttributeFieldSources { get; }

        internal CodeTemplate(ClassDeclarationSyntax classDeclaration, IEnumerable<AttributeFieldSource> fieldSources)
        {
            ClassName = classDeclaration.GetGenericTypeName();
            AttributeFieldSources = fieldSources.ToList();
        }

        internal static string GetFieldTypeFullName(AttributeFieldSource source) => source.TypeName;
        internal static string GetFieldTypeShortName(AttributeFieldSource source) => source.TypeName.Split('.').Last();
        internal static string GetBackingFieldName(AttributeFieldSource source) => source.FieldName;
        internal static string GetLoadedFlagName(AttributeFieldSource source) => source.FieldName + "_loaded";
        internal static string GetPropertyName(AttributeFieldSource source)
        {
            var fieldName = source.FieldName;
            var propName = (fieldName[0] == '_') ? fieldName.Substring(1) : fieldName;
            return propName.ToUpperOnlyFirst();
        }
        internal static string GetMethodName(AttributeFieldSource source)
            => "GetTagValue_" + (source.FieldType == AttributeFieldSource.FieldDeclarationType.BuiltIn ? source.TypeName : "int");

        internal static string GetOptionKey(AttributeFieldSource source) => source.Key;
        internal static string GetOptionId(AttributeFieldSource source) => "0x" + source.Id.ToString("x4");
        internal static bool IsBuiltInType(AttributeFieldSource source) => source.FieldType == AttributeFieldSource.FieldDeclarationType.BuiltIn;

    }
}
