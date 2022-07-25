using System.Threading.Tasks;

namespace MiniURL.Framework.Abstraction
{
    public interface IMiniURLService
    {
        /// <summary>
        /// Method to convert a url to short hand url
        /// </summary>
        /// <param>originalUrl</param>        
        /// <returns>string</returns>
        Task<string> EncryptUrl(string originalUrl);
    }
}
