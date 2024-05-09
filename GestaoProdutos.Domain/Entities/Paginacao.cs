using System.Collections.Generic;

namespace GestaoProdutos.Domain.Entities
{
    public class Paginacao<P>
    {
        public int TotalItems { get; set; }
        public int ItemsByPage { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<P> Items { get; set; }
    }
}
