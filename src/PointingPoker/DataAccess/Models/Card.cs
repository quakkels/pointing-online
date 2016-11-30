using System;

namespace PointingPoker.DataAccess.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public bool IsPointingClosed { get; set; }
        public int TeamId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
