using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace MyGenerator.MetaTagProperty
{
    public partial class CodeTemplate
    {
        internal string Namespace { get; set; } = "";
        internal string ClassName { get; }
        //internal ClassDeclarationSyntax ParentClassDeclaration { get; }
        internal IReadOnlyList<AttributeFieldSource> AttributeFieldSources { get; }

        internal CodeTemplate(ClassDeclarationSyntax classDeclaration, IEnumerable<AttributeFieldSource> fieldSources)
        {
            //ParentClassDeclaration = classDeclaration;
            ClassName = classDeclaration.GetGenericTypeName();
            AttributeFieldSources = fieldSources.ToList();
        }

        internal static string GetFieldType(AttributeFieldSource source) => source.FieldType;
        internal static string GetBackingFieldName(AttributeFieldSource source) => source.FieldName;
        internal static string GetLoadedFlagName(AttributeFieldSource source) => source.FieldName + "_loaded";
        internal static string GetPropertyName(AttributeFieldSource source)
        {
            var fieldName = source.FieldName;
            var propName = (fieldName[0] == '_') ? fieldName.Substring(1) : fieldName;
            return propName.ToUpperOnlyFirst();
        }
        internal static string GetMethodName(AttributeFieldSource source) => "GetTagValue_" + source.FieldType;
        internal static string GetOptionKey(AttributeFieldSource source) => source.Key;
        internal static string GetOptionId(AttributeFieldSource source) => "0x" + source.Id.ToString("x4");

    }
}
