using RestEase;
using System.Threading.Tasks;

namespace Juros.Clients
{
    public interface IJurosClient
    {
        [Get]
        Task<double> GetJuros();
    }
}