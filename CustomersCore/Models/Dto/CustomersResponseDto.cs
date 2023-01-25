namespace CustomersCore.Models.Dto
{
    public class CustomersResponseDto : BaseCustomerResponseDto
    {
        public IEnumerable<CustomerModel> Customers { get; set; }
        public Paging Paging { get; set; }
    }
}
