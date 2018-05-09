using System;
using System.ComponentModel.DataAnnotations;

namespace CoreWebAPI.Models
{
    public class Note
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
