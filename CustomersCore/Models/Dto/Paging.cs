namespace CustomersCore.Models.Dto
{
    public class Paging
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
