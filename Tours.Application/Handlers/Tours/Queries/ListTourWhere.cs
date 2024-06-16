using System.Linq.Expressions;
using Travels.Domain;

namespace Tours.Application.Handlers.Tours.Queries
{
    internal static class ListTourWhere
    {
        public static Expression<Func<Tour, bool>> WhereForClient(ListToursFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return e => freeText == null || e.Name.Contains(freeText);
        }
    }
}
