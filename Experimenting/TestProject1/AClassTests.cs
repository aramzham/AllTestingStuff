using Shouldly;

namespace TestProject1;

public class AClassTests
{
    private readonly A _sut = new();

    [Fact]
    public void Foo_WhenCalled_ReturnsList()
    {
        // Arrange

        // Act
        var result = _sut.Foo();

        // Assert
        result.ShouldBeOfType<List<string>>();
        result.Count.ShouldBe(2);
    }

    [Fact]
    public void Bar_WhenCalled_ReturnsHello()
    {
        // Arrange

        // Act
        var result = _sut.Barton();

        // Assert
        result.ShouldBe(10);
    }
}