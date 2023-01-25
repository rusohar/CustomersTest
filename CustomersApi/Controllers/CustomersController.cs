using Azure.Core;
using CustomersApi.Data;
using CustomersApi.Models.Entities;
using CustomersApi.Services;
using CustomersCore.Models;
using CustomersCore.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerService _customerService;

        public CustomersController(ApplicationDbContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        // GET: api/Customers/customer/
        [HttpGet("customers")]
        public async Task<ActionResult<CustomersResponseDto>> GetCustomers(string name = null,
            string companyName = null, string phone = null, string email = null, int pageNum = 1, string sortOrder="asc", int sortColumn=1)
        {
            var request = new CustomerRequestDto()
            {
                Customer = new CustomerModel(),
                Paging = new Paging(),
                Sorting = new Sorting()
            };
            if (!string.IsNullOrEmpty(name))
                request.Customer.Name = name;
            if (!string.IsNullOrEmpty(companyName))
                request.Customer.CompanyName = companyName;
            if (!string.IsNullOrEmpty(phone))
                request.Customer.Phone = phone;
            if (!string.IsNullOrEmpty(email))
                request.Customer.Email = email;

            request.Paging.PageNum = pageNum;

            request.Sorting.SortingColumn = sortColumn;
            request.Sorting.SortingOrder = sortOrder;

            return await _customerService.GetCustomers(request);
        }

        // PUT: api/Customers/customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("customer/{id}")]
        public async Task<CustomerResponseDto> PutCustomer(long id, CustomerModel request)
        {
            return await _customerService.UpdateCustomer(new CustomerRequestDto
            {
                Customer = request
            });
        }

        // POST: api/Customers/customer/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("customer")]
        public async Task<CustomerResponseDto> PostCustomer(CustomerModel request)
        {
            return await _customerService.CreateCustomer(new CustomerRequestDto
            {
                Customer = request
            });
        }

        // DELETE: api/Customers/customer/5
        [HttpDelete("customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }

        private bool CustomerExists(long id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
