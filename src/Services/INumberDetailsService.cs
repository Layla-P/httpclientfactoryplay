using System.Collections.Generic;
using System.Threading.Tasks;
using HttpClientFactory.Models;

namespace HttpClientFactory.Services
{
    public interface INumberDetailsService
    {
        Task<List<Message>> GetMessagesTo(string number);
    }
}