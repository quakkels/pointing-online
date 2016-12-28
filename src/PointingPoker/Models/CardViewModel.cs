using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class CardViewModel
    {
        [Required]
        public int CreatedBy { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        public int? ClosedBy { get; set; }

        [Required]
        public int TeamId { get; set; }
    }
}
