using System.Collections.Generic;

namespace AuthorizationServer.Models
{
    public class DataTableFilterBase
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string sortDirection { get; set; }
        public string sortColumn { get; set; }
        public List<string> filterWords { get; set; }
    }
}
