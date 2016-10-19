using System;

namespace PointingPoker.DataAccess.Models
{
    public class Point
    {
        public Guid Id { get; set; }
        public Guid PointedBy { get; set; }
        public Guid CardId { get; set; }
        public int Points { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
