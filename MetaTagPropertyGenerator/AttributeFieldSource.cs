using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyGenerator.MetaTagProperty
{
    internal class AttributeFieldSource
    {
        internal enum FieldDeclarationType { BuiltIn, Enum }

        internal string FieldName { get; }
        internal string TypeName { get; }
        internal FieldDeclarationType FieldType { get; }
        internal string Key { get; }
        internal int Id { get; }

        private AttributeFieldSource(string name, string type, FieldDeclarationType decl, string key, int id)
            => (FieldName, TypeName, FieldType, Key, Id) = (name, type, decl, key, id);

        internal static AttributeFieldSource? Create(SemanticModel semanticModel, FieldDeclarationSyntax fieldDeclaration)
        {
            var fieldSymbol = fieldDeclaration.Declaration.Variables.Select(v => semanticModel.GetDeclaredSymbol(v) as IFieldSymbol).FirstOrDefault();
            if (fieldSymbol is null) return null;

            var fieldDecl = (fieldSymbol.Type.TypeKind == TypeKind.Enum) ? FieldDeclarationType.Enum : FieldDeclarationType.BuiltIn;
            var typeName = fieldSymbol.Type.ToString(); //.Split('.').Last();

            var (key, id) = GetOptions(fieldDeclaration);
            return new(fieldSymbol.Name, typeName, fieldDecl, key, id);
        }

        private static (string key, int id) GetOptions(FieldDeclarationSyntax fieldDeclaration)
        {
            var attr = fieldDeclaration.AttributeLists.SelectMany(x => x.Attributes)
                .FirstOrDefault(x => x.Name.ToString() is nameof(MetaTagPropertyGenerator) or MetaTagPropertyGenerator.AttributeName);

            IReadOnlyList<AttributeArgumentSyntax>? args = attr?.ArgumentList?.Arguments;
            if (args is not null && args.Count >= 2)
            {
                var ss = args.Take(2).Select(x => x.ToString()).ToArray();

                if (int.TryParse(ss[1].Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var id))
                    return (ss[0], id);
            }
            return ("", 0);
        }

    }
}
