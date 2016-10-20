using PointingPoker.DataAccess.Models;

namespace PointingPoker.DataAccess.Commands
{
    public interface ICardCommands
    {
        void CreateCard(Card card);
    }
}
