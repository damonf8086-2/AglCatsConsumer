using System.Collections.Generic;
using CatsConsumer.Models;

namespace CatsConsumer.Interfaces
{
    public interface ICatsByOwnerGenderMapper
    {
        ICollection<CatsByOwnerGender> Map(IEnumerable<Person> people);
    }
}
