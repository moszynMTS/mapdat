using System.Collections.Generic;

namespace AuthorizationServer.Models
{
    public class DataTablesResponse<T>
    {
        public List<T> Data { get; set; }
        public int TotalPages { get; set; }
    }
}
