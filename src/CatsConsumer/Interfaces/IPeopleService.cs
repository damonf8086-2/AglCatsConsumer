using System.Collections.Generic;
using System.Threading.Tasks;
using CatsConsumer.Models;

namespace CatsConsumer.Interfaces
{
    public interface IPeopleService
    {
        Task<ICollection<Person>> GetPeople();
    }
}
