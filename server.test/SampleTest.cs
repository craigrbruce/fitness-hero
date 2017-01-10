using Xunit;
using FluentAssertions;

namespace server.test
{
    public class SampleTest
    {
        [Fact]
        public void PassingTest()
        {
            2.Should().Be(2);
        }
    }
}
