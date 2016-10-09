using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class CardInfoViewModel
    {
        [Required]
        public Guid CreatedBy { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        public bool IsPointingClosed { get; set; }
    }
}
