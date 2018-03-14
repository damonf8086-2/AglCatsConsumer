using System;
using System.Collections.Generic;
using CatsConsumer.Interfaces;
using CatsConsumer.Models;

namespace CatsConsumer
{
    public class CatsByOwnerGenderWriter : ICatsByOwnerGenderWriter
    {
        public void Write(IEnumerable<CatsByOwnerGender> catsByOwnerGender)
        {
            if (catsByOwnerGender == null)
            {
                return;
            }

            foreach (var group in catsByOwnerGender)
            {
                Console.WriteLine(group.OwnerGender);

                foreach (var name in group.CatNames)
                {
                    Console.WriteLine($"\t{name}");
                }
            }
        }
    }
}
