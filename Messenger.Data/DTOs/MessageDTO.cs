using System.ComponentModel.DataAnnotations;

namespace Messenger.Data.DTOs
{
    public class MessageDTO
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
