using Wam.Core.ExtensionMethods;
namespace Wam.Core.UnitTests;

[TestClass]
public class StringExtensionsTests
{
    private readonly Action<string> log = Console.WriteLine;

    [TestMethod]
    public void GenerateGameCodeTest()
    {
        var code = StringExtensions.GenerateGameCode(4);
        Assert.AreEqual(4, code.Length);

        code = StringExtensions.GenerateGameCode();
        Assert.AreEqual(6, code.Length);

        var code2 = StringExtensions.GenerateGameCode();
        Assert.AreNotEqual(code, code2);

        log.Invoke(code);
        log.Invoke(code2);
    }
}