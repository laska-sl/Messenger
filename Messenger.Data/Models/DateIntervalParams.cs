using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Data.Models
{
    public class DateIntervalParams
    {
        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}
