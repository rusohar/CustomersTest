namespace CustomersCore.Models.Dto
{
    public class BaseCustomerResponseDto
    {
        public Status Status { get; set; }
    }

    public class Status
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
