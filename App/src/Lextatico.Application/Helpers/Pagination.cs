using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Application.Dtos.Filter;
using Lextatico.Application.Dtos.Response;

namespace Lextatico.Application.Helpers
{
    public class Pagination
    {
        public static PagedResponse CreatePagedReponse(object resultado, PaginationFilterDto pagination, int total)
        {
            var pagedResponse =
                new PagedResponse(resultado, pagination.Page);

            var totalPages = (int)Math.Ceiling((double)total / pagination.Size);

            pagedResponse.TotalPages = totalPages;

            pagedResponse.TotalRecords = total;

            return pagedResponse;
        }
    }
}
