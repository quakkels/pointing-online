using System;

namespace PointingPoker.DataAccess.Models
{
    public class Point
    {
        public int Id { get; set; }
        public int PointedBy { get; set; }
        public int CardId { get; set; }
        public int Points { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
