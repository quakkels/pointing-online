namespace PointingPoker.DataAccess.Models
{
    public class CardScore
    {
        public int CardId { get; set; }
        public int PointedBy { get; set; }
        public int Points { get; set; }
        public string UserName { get; set; }
        public int ClosedBy { get;set; }
    }
}
