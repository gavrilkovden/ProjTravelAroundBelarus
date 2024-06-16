using System.Linq.Expressions;
using Travels.Domain;

namespace Routes.Application.Handlers.Queries
{
    internal static class ListRoteWhere
    {
        public static Expression<Func<Route, bool>> WhereForClient(ListRoutesFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return e => freeText == null || e.Name.Contains(freeText);
        }
    }
}
