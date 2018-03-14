using System;
using System.Collections.Generic;
using System.Linq;
using CatsConsumer.Interfaces;
using CatsConsumer.Models;

namespace CatsConsumer
{
    public class CatsByOwnerGenderMapper : ICatsByOwnerGenderMapper
    {
        private const string CatType = "Cat";

        public ICollection<CatsByOwnerGender> Map(IEnumerable<Person> people) =>
            people?.GroupBy(person => person.Gender, StringComparer.OrdinalIgnoreCase)
                .Select(genderGroup => new CatsByOwnerGender
                {
                    OwnerGender = genderGroup.Key.Capitalize(),

                    CatNames = genderGroup
                        .Where(p => p.Pets != null)
                        .SelectMany(p => p.Pets
                            .Where(pet => CatType.Equals(pet.Type, StringComparison.OrdinalIgnoreCase))
                            .Select(pet => pet.Name))
                        .OrderBy(name => name)
                        .ToArray()

                }).ToArray();
    }
}
