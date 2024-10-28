using Core.Users.Domain;
using System.Linq.Expressions;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Queries;

internal static class ListAttractionWhere
{
    public static Expression<Func<Attraction, bool>> WhereForClient(ListAttractionsFilter filter, Guid currentUserId)
    {
        var freeText = filter.FreeText?.Trim();
        return e => (e.UserId == currentUserId || e.IsApproved == true) &&
                    (freeText == null || e.Name.Contains(freeText)) &&
                    (filter.Region == null || e.Address.Region == filter.Region);
    }

    public static Expression<Func<Attraction, bool>> WhereForAdmin(ListAttractionsFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return e => (freeText == null || e.Name.Contains(freeText)) &&
                    (filter.IsApproved == null || e.IsApproved == filter.IsApproved) &&
                    (filter.Region == null || e.Address.Region == filter.Region);
    }
}

