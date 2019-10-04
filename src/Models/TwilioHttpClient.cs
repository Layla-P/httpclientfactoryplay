using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactory.Models
{
    public class TwilioHttpClient
    {
            public TwilioHttpClient(HttpClient client)
            {
                Client = client;
            }

            public HttpClient Client { get; }
       
    }
}
