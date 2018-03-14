using System.Collections.Generic;

namespace CatsConsumer.Models
{
    public class CatsByOwnerGender
    {
        public string OwnerGender { get; set; }
        public ICollection<string> CatNames { get; set; }
    }
}
