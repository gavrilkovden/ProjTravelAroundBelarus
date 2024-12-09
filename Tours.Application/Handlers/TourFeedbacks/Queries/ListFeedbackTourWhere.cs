using System.Linq.Expressions;
using Travels.Domain;

namespace Tours.Application.Handlers.TourFeedbacks.Queries
{
    internal static class ListFeedbackTourWhere
    {
        public static Expression<Func<TourFeedback, bool>> WhereForClient(ListFeedbackToursFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return e => freeText == null || e.Comment != null && e.Comment.Contains(freeText);
        }

        public static Expression<Func<TourFeedback, bool>> WhereForAdmin(ListFeedbackToursFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return e => freeText == null || e.Comment.Contains(freeText); 
        }
    }
}

