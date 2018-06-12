using System.Threading.Tasks;

namespace EletronicPartsCatalog.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        Task<string> CreateToken(string username);
    }
}