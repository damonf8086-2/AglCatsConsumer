using System.Collections.Generic;
using System.Linq;
using CatsConsumer;
using CatsConsumer.Models;
using FluentAssertions;
using Xunit;

namespace UnitTest
{
    public class CatsByOwnerGenderMapperTest
    {
        [Fact]
        public void Should_return_null_when_people_is_null()
        {
            var subject = new CatsByOwnerGenderMapper();

            var result = subject.Map(null);

            result.Should().BeNull("the people collection was null");
        }

        [Fact]
        public void Should_return_empty_collection_when_people_is_empty()
        {
            var subject = new CatsByOwnerGenderMapper();

            var people = new List<Person>();
            var result = subject.Map(people);

            result.Should().NotBeNull("the people collection was not null");
            result.Count.Should().Be(0, "the people collection was empty");
        }

        [Fact]
        public void Should_return_empty_catNames_collection_when_pets_is_null()
        {
            var subject = new CatsByOwnerGenderMapper();

            var people = new List<Person>
            {
                new Person {Name = "Bob", Gender = "Male", Pets = null}
            };

            var result = subject.Map(people);

            result.Should().NotBeNull("the people collection was not null");
            result.Count.Should().Be(1, "the people collection has 1 entry");

            var group = result.First();
            group.CatNames.Should().NotBeNull();
            group.CatNames.Count.Should().Be(0);
        }

        [Fact]
        public void Should_group_cats_by_owner_gender()
        {
            var subject = new CatsByOwnerGenderMapper();

            var people = new List<Person>
            {
                new Person {
                    Name = "Bob",
                    Gender = "Male",
                    Pets = new List<Pet>
                    {
                        new Pet { Name = "Garfield", Type = "Cat"},
                        new Pet { Name = "Fido", Type = "Dog"},
                    }
                },

                new Person {
                    Name = "Samantha",
                    Gender = "Female",
                    Pets = new List<Pet>
                    {
                        new Pet { Name = "Tabby", Type = "Cat"}
                    }
                },

                new Person {
                    Name ="Alice",
                    Gender = "Female",
                    Age = 64,
                    Pets = new List<Pet>
                    {
                        new Pet { Name="Simba", Type= "Cat"},
                        new Pet { Name="Nemo", Type= "Fish"},
                    }
                }
            };

            var result = subject.Map(people);

            result.Should().NotBeNull("the people collection was not null");
            result.Count.Should().Be(2, "the people collection has 2 distinct genders");

            var maleGroup = result.FirstOrDefault(x => x.OwnerGender == "Male");
            maleGroup.Should().NotBeNull();
            maleGroup.CatNames.Should().NotBeNull();
            maleGroup.CatNames.Count.Should().Be(1);
            maleGroup.CatNames.Should().Contain("Garfield");

            var femaleGroup = result.FirstOrDefault(x => x.OwnerGender == "Female");
            femaleGroup.Should().NotBeNull();
            femaleGroup.CatNames.Should().NotBeNull();
            femaleGroup.CatNames.Count.Should().Be(2);
            femaleGroup.CatNames.Should().Contain("Simba");
            femaleGroup.CatNames.Should().Contain("Tabby");
        }

        [Fact]
        public void Should_ignore_case()
        {
            var subject = new CatsByOwnerGenderMapper();

            var people = new List<Person>
            {
                new Person {
                    Name = "Bob",
                    Gender = "Male",
                    Pets = new List<Pet>
                    {
                        new Pet { Name = "Garfield", Type = "Cat"},
                    }
                },

                new Person {
                    Name = "Bill",
                    Gender = "male",
                    Pets = new List<Pet>
                    {
                        new Pet { Name = "Leo", Type = "cat"},
                    }
                },

                new Person {
                    Name = "Samantha",
                    Gender = "female",
                    Pets = new List<Pet>
                    {
                        new Pet { Name = "Tabby", Type = "Cat"}
                    }
                },

                new Person {
                    Name ="Alice",
                    Gender = "Female",
                    Age = 64,
                    Pets = new List<Pet>
                    {
                        new Pet { Name="Simba", Type= "cat"},
                    }
                }
            };

            var result = subject.Map(people);

            result.Should().NotBeNull("the people collection was not null");
            result.Count.Should().Be(2, "the people collection has 2 distinct genders");

            var maleGroup = result.FirstOrDefault(x => x.OwnerGender == "Male");
            maleGroup.Should().NotBeNull();
            maleGroup.CatNames.Should().NotBeNull();
            maleGroup.CatNames.Count.Should().Be(2);
            maleGroup.CatNames.Should().Contain("Garfield");
            maleGroup.CatNames.Should().Contain("Leo");

            var femaleGroup = result.FirstOrDefault(x => x.OwnerGender == "Female");
            femaleGroup.Should().NotBeNull();
            femaleGroup.CatNames.Should().NotBeNull();
            femaleGroup.CatNames.Count.Should().Be(2);
            femaleGroup.CatNames.Should().Contain("Tabby");
            femaleGroup.CatNames.Should().Contain("Simba");
        }

        [Fact]
        public void Should_sort_cats_alphabetically()
        {
            var subject = new CatsByOwnerGenderMapper();

            var people = new List<Person>
            {
                new Person {
                    Name = "Bob",
                    Gender = "Male",
                    Pets = new List<Pet>
                    {
                        new Pet { Name = "Max", Type = "Cat"},
                        new Pet { Name = "Adam", Type = "Cat"},
                        new Pet { Name = "Garfield", Type = "Cat"},
                        new Pet { Name = "Fido", Type = "Dog"},
                    }
                },

                new Person {
                    Name = "Samantha",
                    Gender = "Female",
                    Pets = new List<Pet>
                    {
                        new Pet { Name = "Tabby", Type = "Cat"},
                        new Pet { Name = "Alexis", Type = "Cat"}
                    }
                },

                new Person {
                    Name ="Alice",
                    Gender = "Female",
                    Age = 64,
                    Pets = new List<Pet>
                    {
                        new Pet { Name="Simba", Type= "Cat"},
                        new Pet { Name="Zeus", Type= "Cat"},
                        new Pet { Name="Nemo", Type= "Fish"},
                    }
                }
            };

            var result = subject.Map(people);

            result.Should().NotBeNull("the people collection was not null");
            result.Count.Should().Be(2, "the people collection has 2 distinct genders");

            var maleGroup = result.FirstOrDefault(x => x.OwnerGender == "Male");
            maleGroup.Should().NotBeNull();
            maleGroup.CatNames.Should().NotBeNull();
            maleGroup.CatNames.Count.Should().Be(3);
            maleGroup.CatNames.Should().BeInAscendingOrder("cat names should be in alphabetical order");

            var femaleGroup = result.FirstOrDefault(x => x.OwnerGender == "Female");
            femaleGroup.Should().NotBeNull();
            femaleGroup.CatNames.Should().NotBeNull();
            femaleGroup.CatNames.Count.Should().Be(4);
            femaleGroup.CatNames.Should().BeInAscendingOrder("cat names should be in alphabetical order");
        }
    }
}
