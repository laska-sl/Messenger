using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Data.Models
{
    public class Message
    {
        [Required]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
