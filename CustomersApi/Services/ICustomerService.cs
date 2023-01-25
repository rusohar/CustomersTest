using CustomersCore.Models.Dto;

namespace CustomersApi.Services
{
    public interface ICustomerService
    {
        Task<CustomersResponseDto> GetCustomers(CustomerRequestDto request);
        Task<CustomerResponseDto> GetCustomerById(long id);
        Task<CustomerResponseDto> CreateCustomer(CustomerRequestDto request);
        Task<CustomerResponseDto> UpdateCustomer(CustomerRequestDto request);
        Task DeleteCustomer(long id);
    }
}