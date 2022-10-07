using System;
using System.Threading.Tasks;

namespace Talabat.BLL.Interfaces
{
    public interface IResponseCacheService
    {
        Task CasheResponceAsync(string casheKey, object response, TimeSpan timeToLive);
        Task<string> GetCashedResponse(string casheKey);
    }
}
