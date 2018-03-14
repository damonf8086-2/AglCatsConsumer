using System;
using System.Collections.Generic;
using System.IO;
using CatsConsumer;
using CatsConsumer.Models;
using FluentAssertions;
using Xunit;

namespace UnitTest
{
    public class CatsByOwnerGenderWriterTest
    {
        [Fact]
        public void Should_write_nothing_when_collection_is_null()
        {
            var subject = new CatsByOwnerGenderWriter();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                subject.Write(null);

                var result = sw.ToString();

                result.Should().Be(string.Empty);
            }
        }

        [Fact]
        public void Should_write_cats_by_owner_gender()
        {
            var catsByOwnerGender = new List<CatsByOwnerGender>
            {
                new CatsByOwnerGender
                {
                    OwnerGender = "Male",
                    CatNames = new List<string> {"Adam", "Garfield"}
                },

                new CatsByOwnerGender
                {
                    OwnerGender = "Female",
                    CatNames = new List<string> {"Simba"}
                }
            };

            var subject = new CatsByOwnerGenderWriter();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                subject.Write(catsByOwnerGender);

                var result = sw.ToString();

                result.Should().NotBeNullOrWhiteSpace();

                var parts = result.Split(Environment.NewLine);
                parts.Should().ContainInOrder("Male", "\tAdam", "\tGarfield", "Female", "\tSimba");
            }
        }
    }
}
