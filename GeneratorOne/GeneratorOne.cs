using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GeneratorOne;

[Generator]
public class GeneratorOne : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization(GenerateAttributes);
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    private void GenerateAttributes(GeneratorPostInitializationContext obj)
    {
        obj.AddSource("TestAttribute", @"
    namespace TestSpace {    public class TestAttribute : Attribute
    {
    }
}
    ");
    }

    public void Execute(GeneratorExecutionContext context)
    {
         var receiver = context.SyntaxReceiver as SyntaxReceiver;
                        if (receiver == null) return;
        
                        foreach (var (type, attr) in receiver.Targets)
                        {
                            context.AddSource(type.Identifier+ "genrated.cs",$@"
                                public partial class {type.Identifier} " + @"{
                                   public void SayHello() {
                                    Console.WriteLine(" + "\"Hello from" + $"{type.Identifier}" +"\"" +@");
                                } 
                            }
");
                        }
    }
    class SyntaxReceiver : ISyntaxReceiver
            {
                public List<(ClassDeclarationSyntax type, AttributeSyntax attr)> Targets { get; } = new();
    
                public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
                {
                    if (syntaxNode is ClassDeclarationSyntax s && s.AttributeLists.Count > 0)
                    {
                        var attr = s.AttributeLists.SelectMany(x => x.Attributes).FirstOrDefault(x => x.Name.ToString() is "Test");
                        if (attr != null)
                        {
                            Targets.Add((s, attr));
                        }
                    }
                }
            }
}