using System;

namespace PointingPoker.DataAccess.Queries
{
    public interface ICardQueries
    {
        bool DoesCardCreatorExist(Guid creatorId);
    }
}
