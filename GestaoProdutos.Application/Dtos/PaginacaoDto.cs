using System.Collections.Generic;

namespace GestaoProdutos.Application.Dtos
{
    public class PaginacaoDto<P>
    {
        public int TotalItems { get; set; }
        public int ItemsByPage { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<P> Items { get; set; }
    }
}
