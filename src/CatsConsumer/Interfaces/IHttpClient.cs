using System.Threading.Tasks;

namespace CatsConsumer.Interfaces
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
    }
}
