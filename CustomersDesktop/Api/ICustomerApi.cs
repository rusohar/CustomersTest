using CustomersCore.Models;
using CustomersCore.Models.Dto;
using Refit;
using System.Collections.Generic;
using System.Security.RightsManagement;
using System.Threading.Tasks;

namespace CustomersDesktop.Api
{
    public interface ICustomerApi
    {
        [Get("/api/customers/customers")]
        Task<CustomersResponseDto> GetCustomers(string name = null,
            string companyName = null, string phone = null, string email = null, int pageNum = 1, string sortOrder = "asc", int sortColumn = 1);
        [Post("/api/customers/customer")]
        Task<CustomerResponseDto> CreateCustomer([Body]CustomerModel data);
        [Put("/api/customers/customer/{id}")]
        Task<CustomerResponseDto> EditCustomer([Body] CustomerModel data, long id);
        [Delete("/api/customers/customer/{id}")]
        Task DeleteCustomer(long id);
    }
}
