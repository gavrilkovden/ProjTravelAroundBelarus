using System.Linq.Expressions;
using Travels.Domain;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter
{
    internal static class ListFeedbackAttractionWhere
    {
        public static Expression<Func<AttractionFeedback, bool>> WhereForClient(ListFeedbackAttractionsFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return e => freeText == null || e.Comment != null && e.Comment.Contains(freeText);
        }
    }
}
