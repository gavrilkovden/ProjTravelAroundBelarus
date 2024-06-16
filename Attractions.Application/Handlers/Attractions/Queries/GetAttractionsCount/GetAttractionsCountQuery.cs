using Core.Auth.Application.Attributes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttractionsCount
{
    [RequestAuthorize]
    public class GetAttractionsCountQuery : ListAttractionsFilter, IRequest<int>
    {

    }
}
