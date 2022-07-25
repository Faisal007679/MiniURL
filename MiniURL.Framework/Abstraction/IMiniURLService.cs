using System.Threading.Tasks;

namespace MiniURL.Framework.Abstraction
{
    public interface IMiniURLService
    {
        Task<string> EncryptUrl(string originalUrl);
    }
}
