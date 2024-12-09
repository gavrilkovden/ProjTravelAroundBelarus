using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Queries.ImageFilter
{
    internal static class ListImageWhere
    {
        public static Expression<Func<Image, bool>> WhereForClient(ListImagesFilter filter, Guid currentUserId)
        {
            var freeText = filter.FreeText?.Trim();
            return e => (e.IsApproved == true) &&
                        (freeText == null || e.ImagePath.Contains(freeText));
        }

        public static Expression<Func<Image, bool>> WhereForAdmin(ListImagesFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return e => (freeText == null || e.ImagePath.Contains(freeText)) &&
                        (filter.IsApproved == null || e.IsApproved == filter.IsApproved);
        }
    }
}
