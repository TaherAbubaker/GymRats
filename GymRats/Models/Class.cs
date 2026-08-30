using System;
using System.ComponentModel.DataAnnotations;

namespace GymRats.Models
{
    // Owned by: Abood
    // Table: Classes
    public class Class
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Trainer { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}
