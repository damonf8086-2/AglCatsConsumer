using CatsConsumer;
using FluentAssertions;
using Xunit;

namespace UnitTest
{
    public class StringExtensions
    {
        [Fact]
        public void Should_return_null_when_input_is_null()
        {
            string input = null;

            var result = input.Capitalize();

            result.Should().BeNull();
        }

        [Fact]
        public void Should_capitalize_when_input_is_1_char()
        {
            const string input = "a";

            var result = input.Capitalize();

            result.Should().Be("A");
        }

        [Fact]
        public void Should_capitalize_input()
        {
            const string input = "abC";

            var result = input.Capitalize();

            result.Should().Be("Abc");
        }
    }
}
