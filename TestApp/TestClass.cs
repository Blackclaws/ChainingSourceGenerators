using TestSpace;

var test = new BClass();

public class BClass
{
    public BClass()
    {
        var a = new TestClass();
        a.SayHello();
        var b = new Wow();
        b.SayHello();
    }
}

[Test]
public partial class TestClass
{
    
}
