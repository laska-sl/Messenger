using System;

namespace Messenger.Data.Models
{
    public class Message
    {
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Number { get; set; }
    }
}
