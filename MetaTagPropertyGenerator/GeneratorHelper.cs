using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGenerator.MetaTagProperty
{
    static class StringExtension
    {
        public static string ToLowerOnlyFirst(this string str) => Char.ToLower(str[0]) + str.Substring(1);
        public static string ToUpperOnlyFirst(this string str) => Char.ToUpper(str[0]) + str.Substring(1);

        public static string JoinWithCommas(this IEnumerable<string> ss) => string.Join(", ", ss);
    }

    static class RoslynExtension
    {
        // Code from: https://github.com/YairHalberstadt/stronginject/blob/779a38e7e74b92c87c86ded5d1fef55744d34a83/StrongInject/Generator/RoslynExtensions.cs#L166
        public static string FullName(this INamespaceSymbol @namespace) => @namespace.ToDisplayString(new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces));

        // Code from: https://github.com/YairHalberstadt/stronginject/blob/779a38e7e74b92c87c86ded5d1fef55744d34a83/StrongInject/Generator/RoslynExtensions.cs#L69
        public static IEnumerable<INamedTypeSymbol> GetContainingTypesAndThis(this INamedTypeSymbol? namedType)
        {
            var current = namedType;
            while (current is not null)
            {
                yield return current;
                current = current.ContainingType;
            }
        }

        // Code from: https://github.com/YairHalberstadt/stronginject/blob/779a38e7e74b92c87c86ded5d1fef55744d34a83/StrongInject/Generator/SourceGenerator.cs#L87
        public static string GenerateHintName(this INamedTypeSymbol container)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(container.ContainingNamespace.FullName());
            foreach (var type in container.GetContainingTypesAndThis().Reverse())
            {
                stringBuilder.Append(".");
                stringBuilder.Append(type.Name);
                if (type.TypeParameters.Length > 0)
                {
                    stringBuilder.Append("_");
                    stringBuilder.Append(type.TypeParameters.Length);
                }
            }
            stringBuilder.Append(".g.cs");
            return stringBuilder.ToString();
        }

        public static string? NullIfEmpty(this string? value) => string.IsNullOrEmpty(value) ? null : value;
    }

    static class SyntaxExtension
    {
        public static string GetGenericTypeName(this TypeDeclarationSyntax typeDecl)
        {
            if (typeDecl.TypeParameterList is null)
                return typeDecl.Identifier.Text;

            var param = string.Join(", ", typeDecl.TypeParameterList.Parameters.Select(p => p.Identifier.Text));
            return typeDecl.Identifier.Text + "<" + param + ">";
        }
    }

    //class RecordDefinition
    //{
    //    public StructDeclarationSyntax ParentSyntax { get; }
    //    public IReadOnlyList<SimpleProperty> Properties { get; }
    //    public bool IsConstructorDeclared { get; }

    //    public RecordDefinition(StructDeclarationSyntax parentDecl, StructDeclarationSyntax recordDecl)
    //    {
    //        ParentSyntax = parentDecl;
    //        Properties = SimpleProperty.New(recordDecl).ToArray();
    //        IsConstructorDeclared = GetIsConstructorDeclared(ParentSyntax, Properties);
    //    }

    //    private bool GetIsConstructorDeclared(StructDeclarationSyntax structDecl, IReadOnlyList<SimpleProperty> properties)
    //    {
    //        var ctorDeclarationSyntaxs = structDecl.Members.Where(mem => mem.Kind() == SyntaxKind.ConstructorDeclaration)
    //            .OfType<ConstructorDeclarationSyntax>();

    //        foreach (var syntax in ctorDeclarationSyntaxs)
    //        {
    //            var typeNames = syntax.ParameterList.Parameters.Select(x => x.Type?.ToString() ?? "");
    //            var props = properties.Select(p => p.Type.ToString());
    //            if (typeNames?.SequenceEqual(props) ?? false)
    //                return true;
    //        }
    //        return false;
    //    }
    //}

    //class SimpleProperty
    //{
    //    public TypeSyntax Type { get; }
    //    public string Name { get; }
    //    public SyntaxTriviaList LeadingTrivia { get; }
    //    public SyntaxTriviaList TrailingTrivia { get; }

    //    public SimpleProperty(FieldDeclarationSyntax d)
    //    {
    //        Type = d.Declaration.Type;
    //        Name = d.Declaration.Variables[0].Identifier.Text;
    //        LeadingTrivia = d.GetLeadingTrivia();
    //        TrailingTrivia = d.GetTrailingTrivia();
    //    }

    //    public static IEnumerable<SimpleProperty> New(StructDeclarationSyntax decl)
    //        => decl.Members.OfType<FieldDeclarationSyntax>().Select(d => new SimpleProperty(d));
    //}
}

namespace System.CodeDom.Compiler
{
    public class CompilerError
    {
        public string? ErrorText { get; set; }
        public bool IsWarning { get; set; }
    }

    public class CompilerErrorCollection
    {
        public void Add(CompilerError error) { }
    }
}
