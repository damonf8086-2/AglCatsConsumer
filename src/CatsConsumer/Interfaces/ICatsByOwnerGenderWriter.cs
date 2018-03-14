using System.Collections.Generic;
using CatsConsumer.Models;

namespace CatsConsumer.Interfaces
{
    public interface ICatsByOwnerGenderWriter
    {
        void Write(IEnumerable<CatsByOwnerGender> catsByOwnerGender);
    }
}
