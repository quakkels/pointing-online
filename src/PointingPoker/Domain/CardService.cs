using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using System.Collections.Generic;
using System.Linq;

namespace PointingPoker.Domain
{
    public class CardService : ICardService
    {
        private readonly ICardCommands _cardCommands;
        private readonly ICardQueries _cardQueries;
        private readonly ITeamQueries _teamQueries;

        public CardService(
            ICardCommands cardCommands, 
            ICardQueries cardQueries,
            ITeamQueries teamQueries)
        {
            _cardCommands = cardCommands;
            _cardQueries = cardQueries;
            _teamQueries = teamQueries;
        }

        public int CreateCard(Card card)
        {
            if (
                string.IsNullOrEmpty(card.Description)
                || card.TeamId == 0)
            {
                return 0;
            }

            if (!_cardQueries.DoesCardCreatorExist(card.CreatedBy))
            {
                return 0;
            }

            var id = _cardCommands.CreateCard(card);
            card.Id = id;
            return id;
        }

        public IEnumerable<Card> GetCardsToPointForTeam(int userId, int teamId)
        {
            return _cardQueries.GetCardsToPointForTeam(userId, teamId);
        }

        public IEnumerable<Card> GetOpenCardsForTeam(int teamId)
        {
            return _cardQueries.GetOpenCardsForTeam(teamId);
        }

        public Card GetCard(int cardId)
        {
            return _cardQueries.GetCard(cardId);
        }

        public bool ClosePointing(int cardId, int userId)
        {
            if (cardId < 1 || userId < 1)
            {
                return false;
            }

            _cardCommands.ClosePointing(cardId, userId);
            return true;
        }

        public IEnumerable<Card> GetClosedCardsForTeam(int teamId, int userId)
        {
            var teamsForUser = _teamQueries.GetTeamsByUser(userId);

            if (teamsForUser.All(x => x.Id != teamId))
            {
                return new List<Card>();
            }
            
            return _cardQueries.GetClosedCardsForTeam(teamId);
        }
    }
}
