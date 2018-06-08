using System.Threading.Tasks;

namespace EletronicPartsCatalog.Features.Profiles
{
    public interface IProfileReader
    {
        Task<ProfileEnvelope> ReadProfile(string username);
    }
}