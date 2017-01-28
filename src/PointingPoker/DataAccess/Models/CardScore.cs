namespace PointingPoker.DataAccess.Models
{
    public class CardScore
    {
        public int CardId { get; set; }
        public int PointedBy { get; set; }
        public int Points { get; set; }
    }
}
