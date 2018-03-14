using System;
using System.Threading.Tasks;
using CatsConsumer.Interfaces;

namespace CatsConsumer
{
    public class AppRunner : IAppRunner
    {
        private readonly IPeopleService _peopleService;
        private readonly ICatsByOwnerGenderMapper _catsMapper;
        private readonly ICatsByOwnerGenderWriter _catsWriter; 

        public AppRunner(
            IPeopleService peopleService,
            ICatsByOwnerGenderMapper catsMapper,
            ICatsByOwnerGenderWriter catsWriter
            )
        {
            _peopleService = peopleService ?? throw new ArgumentNullException(nameof(peopleService));
            _catsMapper = catsMapper ?? throw new ArgumentNullException(nameof(catsMapper));
            _catsWriter = catsWriter ?? throw new ArgumentNullException(nameof(catsWriter));
        }

        public async Task RunAsync()
        {
            var peopleWithPets = await _peopleService.GetPeople();
            var catsByOwnerGender = _catsMapper.Map(peopleWithPets);

            _catsWriter.Write(catsByOwnerGender);
        }
    }
}
