using PointingPoker.DataAccess.Models;

namespace PointingPoker.Domain
{
    public interface ICardService
    {
        bool CreateCard(Card card);
    }
}
