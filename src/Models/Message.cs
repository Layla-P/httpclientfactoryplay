using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpClientFactory.Models
{
    public class Wrapper
    {
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
        [JsonProperty("next_page_uri")]
        public string NexPagUri { get; set; }

    }
    public class Message
    {
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("date_sent")]
        public DateTime DateSent { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
