namespace CustomersCore.Models.Dto
{
    public class CustomerRequestDto
    {
        public CustomerModel Customer { get; set; }
        public Paging Paging { get; set; }
        public Sorting Sorting { get; set; }
    }
}
