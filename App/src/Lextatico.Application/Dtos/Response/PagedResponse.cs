using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Application.Dtos.Response
{
    public class PagedResponse : Response
    {
        public PagedResponse(object data, int page)
            : base(data)
        {
            Page = page;
        }

        public int Page { get; set; }
        public int Size => ((IEnumerable<object>)Data).Count();
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}