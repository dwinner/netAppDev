using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoArrangeRefactoring
{
   public sealed class AutoArrangeCaptureWalker : CSharpSyntaxWalker
   {
      public List<ConstructorDeclarationSyntax> Constructors { get; } = new List<ConstructorDeclarationSyntax>();
      public List<EnumDeclarationSyntax> Enums { get; }=new List<EnumDeclarationSyntax>();
      public List<FieldDeclarationSyntax> Fields { get; }=new List<FieldDeclarationSyntax>();
      public List<EventDeclarationSyntax> Events { get; set; }=new List<EventDeclarationSyntax>();
      public List<PropertyDeclarationSyntax> Properties { get; }=new List<PropertyDeclarationSyntax>();
      public List<MethodDeclarationSyntax> Methods { get; }=new List<MethodDeclarationSyntax>();
      public TypeDeclarationSyntax Target { get; set; }
      public List<AutoArrangeCaptureWalker> Types { get; }=new List<AutoArrangeCaptureWalker>();

      public override void VisitClassDeclaration(ClassDeclarationSyntax node)
      {
         Target = node;
         AutoArrangeCaptureWalker captureWalker=new AutoArrangeCaptureWalker();
         captureWalker.VisitClassDeclaration(node);
         Types.Add(captureWalker);
         base.VisitClassDeclaration(node);
      }

      public override void VisitStructDeclaration(StructDeclarationSyntax node)
      {
         Target = node;
         AutoArrangeCaptureWalker captureWalker=new AutoArrangeCaptureWalker();
         captureWalker.VisitStructDeclaration(node);
         Types.Add(captureWalker);
         base.VisitStructDeclaration(node);
      }

      public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
      {
         Constructors.Add(node);
         base.VisitConstructorDeclaration(node);
      }

      public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
      {
         Enums.Add(node);
         base.VisitEnumDeclaration(node);
      }

      public override void VisitEventDeclaration(EventDeclarationSyntax node)
      {
         Events.Add(node);
         base.VisitEventDeclaration(node);
      }

      public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
      {
         Fields.Add(node);
         base.VisitFieldDeclaration(node);
      }

      public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
      {
         Methods.Add(node);
         base.VisitMethodDeclaration(node);
      }

      public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
      {
         Properties.Add(node);
         base.VisitPropertyDeclaration(node);
      }

      
   }
}