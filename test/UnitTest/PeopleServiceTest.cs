using System.Linq;
using System.Threading.Tasks;
using CatsConsumer;
using CatsConsumer.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTest
{
    public class PeopleServiceTest
    {
        [Fact]
        public async void Should_return_null_when_httpClient_returns_null()
        {
            var configuration = A.Fake<IConfigurationRoot>();

            var httpClient = A.Fake<IHttpClient>();

            const string jsonString = null;

            A.CallTo(() => httpClient.GetStringAsync(A<string>.Ignored)).Returns(jsonString);

            var subject = new PeopleService(configuration, httpClient);

            var result = await subject.GetPeople();

            result.Should().BeNull("the json string was null");
        }

        [Fact]
        public async void Should_return_null_when_httpClient_returns_empty_string()
        {
            var configuration = A.Fake<IConfigurationRoot>();

            var httpClient = A.Fake<IHttpClient>();

            var jsonString = string.Empty;

            A.CallTo(() => httpClient.GetStringAsync(A<string>.Ignored)).Returns(jsonString);

            var subject = new PeopleService(configuration, httpClient);

            var result = await subject.GetPeople();

            result.Should().BeNull("the json string was empty");
        }

        // This test helps guard against breaking changes/bugs in the JSON library
        // we are using.
        [Fact]
        public async Task Should_deserialize_people()
        {
            var configuration = A.Fake<IConfigurationRoot>();

            var httpClient = A.Fake<IHttpClient>();

            const string jsonString = @"[
                   {""name"":""Bob"",
                    ""gender"":""Male"",
                    ""age"":23,
                    ""pets"":[
                       {""name"":""Garfield"",
                        ""type"":""Cat""}]
                   },
                   {""name"":""Steve"",
                    ""gender"":""Male"",
                    ""age"":45,
                    ""pets"":null
                   }]";

            A.CallTo(() => httpClient.GetStringAsync(A<string>.Ignored)).Returns(jsonString);

            var subject = new PeopleService(configuration, httpClient);

            var result = await subject.GetPeople();

            result.Should().NotBeNull();
            result.Count.Should().Be(2);

            var people = result.ToArray();

            var bob = people[0];
            bob.Should().NotBeNull();
            bob.Name.Should().Be("Bob");
            bob.Gender.Should().Be("Male");
            bob.Pets.Count.Should().Be(1);

            var garfield = bob.Pets.First();
            garfield.Name.Should().Be("Garfield");
            garfield.Type.Should().Be("Cat");

            var steve = people[1];
            steve.Should().NotBeNull();
            steve.Name.Should().Be("Steve");
            steve.Pets.Should().BeNull();
        }
    }
}
