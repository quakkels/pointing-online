using System;

namespace PointingPoker.DataAccess.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsPointingClosed { get; set; }
        public Guid TeamId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
