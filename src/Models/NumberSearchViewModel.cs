using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientFactory.Models
{
    public class NumberSearchViewModel
    {
        [Required]
        [Display(Name = "Number")]
        public string Number { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
