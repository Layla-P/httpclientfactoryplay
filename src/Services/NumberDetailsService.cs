using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HttpClientFactory.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HttpClientFactory.Services
{
    public class NumberDetailsService : INumberDetailsService
    {
        private readonly TwilioAccount _twilioAccount;
        private readonly TwilioHttpClient _twilioHttpClient;

        public NumberDetailsService(IOptions<TwilioAccount> options, TwilioHttpClient twilioHttpClient)
        {
            if(options==null){
                throw new ArgumentException(nameof(options));
            }
            _twilioAccount = options.Value;
            _twilioHttpClient = twilioHttpClient ?? throw new ArgumentException(nameof(twilioHttpClient));
        }

        public async Task<List<Message>> GetMessagesTo(string number)
        {
            var path =
                $"Accounts/{_twilioAccount.AccountSid}/Messages.json?To={HttpUtility.UrlEncode(number)}";
            
            var responseMessage = await _twilioHttpClient.Client.GetAsync(path);
           
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<Wrapper>(jsonContent).Messages;
                return messages;
            }

            return new List<Message>();
        }


    }
}

//https://stackoverflow.com/questions/19581820/how-to-convert-json-array-to-list-of-objects-in-c-sharp