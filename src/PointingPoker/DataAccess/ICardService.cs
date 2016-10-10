using PointingPoker.DataAccess.Models;

namespace PointingPoker.DataAccess
{
    public interface ICardService
    {
        bool CreateCard(Card card);
    }
}
