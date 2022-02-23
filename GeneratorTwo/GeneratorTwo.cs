using Microsoft.CodeAnalysis;

namespace GeneratorTwo;

[Generator]
public class GeneratorTwo : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization(GenerateAttributes);
    }

    private void GenerateAttributes(GeneratorPostInitializationContext obj)
    {
        obj.AddSource("TestAttribute", @"
using TestSpace;
    [Test]
    public partial class Wow 
    {
    }
    ");
    }

    public void Execute(GeneratorExecutionContext context)
    {
    }
}
