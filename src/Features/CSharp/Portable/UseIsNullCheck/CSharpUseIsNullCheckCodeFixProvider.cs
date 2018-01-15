﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Composition;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.UseIsNullCheck;

namespace Microsoft.CodeAnalysis.CSharp.UseIsNullCheck
{
    [ExportCodeFixProvider(LanguageNames.CSharp), Shared]
    internal class CSharpUseIsNullCheckCodeFixProvider : AbstractUseIsNullCheckCodeFixProvider
    {
        protected override string GetIsNullTitle()
            => CSharpFeaturesResources.Use_is_null_check;

        protected override string GetIsNotNullTitle()
            => GetIsNullTitle();

        private static SyntaxNode CreateNullCheck(SyntaxNode argument, SyntaxKind comparisonOperator)
            => SyntaxFactory.BinaryExpression(
                comparisonOperator,
                (ExpressionSyntax)argument,
                SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)).Parenthesize();

        protected override SyntaxNode CreateIsNullCheck(SyntaxNode argument)
            => CreateNullCheck(argument, SyntaxKind.EqualsExpression);

        protected override SyntaxNode CreateIsNotNullCheck(SyntaxNode notExpression, SyntaxNode argument)
            => CreateNullCheck(argument, SyntaxKind.NotEqualsExpression);
    }
}
