using CustomersApi.Data;
using CustomersApi.Models.Entities;
using CustomersCore.Models;
using CustomersCore.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace CustomersApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomersResponseDto> GetCustomers(CustomerRequestDto request)
        {
            CustomersResponseDto response = new CustomersResponseDto();
            try
            {
                var customers = _context.Customers
                    .FromSql(
                        $"spExtendedSearch @Name={request?.Customer?.Name}, @CompanyName={request?.Customer?.CompanyName}, @Phone={request?.Customer?.Phone}, @Email={request?.Customer?.Email}, @PageNo={request?.Paging?.PageNum ?? 1}, @SortOrder={request?.Sorting?.SortingOrder ?? "asc"}, @SortColumn={request?.Sorting?.SortingColumn ?? 1}");

                response.Customers = customers.AsEnumerable().Select(x => new CustomerModel
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    Email = x.Email,
                    Name = x.Name,
                    Phone = x.Phone
                });

                var totalCount = await _context.Customers.CountAsync();
                var totalPages = totalCount / 20;
                if (totalCount % 20>0)
                {
                    totalPages++;
                }
                response.Paging = new Paging
                {
                    PageNum = request.Paging.PageNum < 1 ? 1 : request.Paging.PageNum,
                    PageSize = 20,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                };
            }
            catch (Exception ex)
            {
                response.Status = new Status
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

            return response;
            //IEnumerable<CustomerModel> customers;

            //if (_context.Customers == null)
            //{
            //    customers = Enumerable.Empty<CustomerModel>();
            //}
            //else
            //{
            //    customers = _context.Customers.Select(x => new CustomerModel
            //    {
            //        Id = x.Id,
            //        CompanyName = x.CompanyName,
            //        Email = x.Email,
            //        Name = x.Name,
            //        Phone = x.Phone
            //    }).AsEnumerable();
            //}

            //return new CustomersResponseDto
            //{
            //    Customers = customers
            //};
        }

        public async Task<CustomerResponseDto> GetCustomerById(long id)
        {
            CustomerModel customerModel = null;

            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                customerModel = new CustomerModel
                {
                    Id = customer.Id,
                    CompanyName = customer.CompanyName,
                    Email = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone
                };
            }


            return customerModel != null
                ? new CustomerResponseDto { Customer = customerModel }
                : new CustomerResponseDto
                { Status = new Status { IsSuccess = false, Message = $"Customer with id = {id} not found" } };
        }

        public async Task<CustomerResponseDto> CreateCustomer(CustomerRequestDto request)
        {
            CustomerResponseDto response = new CustomerResponseDto();

            Customer entity = new Customer
            {
                CompanyName = request.Customer.CompanyName,
                Email = request.Customer.Email,
                Name = request.Customer.Name,
                Phone = request.Customer.Phone
            };

            _context.Customers.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
                response.Customer = EntityToDto(entity);
            }
            catch (Exception e)
            {
                response.Status = new Status { Message = e.Message, IsSuccess = false };
            }

            return response;
        }

        public async Task<CustomerResponseDto> UpdateCustomer(CustomerRequestDto request)
        {
            CustomerResponseDto response = new CustomerResponseDto();

            var entity = await _context.Customers.FindAsync(request.Customer.Id);

            if (entity != null)
            {
                entity.Phone = request.Customer.Phone;
                entity.Name = request.Customer.Name;
                entity.Phone = request.Customer.Phone;
                entity.Email = request.Customer.Email;

                try
                {
                    await _context.SaveChangesAsync();
                    response.Customer = EntityToDto(entity);
                }
                catch (Exception e)
                {
                    if (!CustomerExists(request.Customer.Id))
                    {
                        response.Status = new Status
                        {
                            Message = $"Customer with id = {request.Customer.Id} not found",
                            IsSuccess = false
                        };
                    }
                    else
                    {
                        response.Status = new Status { Message = e.Message, IsSuccess = false };
                    }
                }
            }

            return response;
        }

        public async Task DeleteCustomer(long id)
        {
            CustomerResponseDto response = new CustomerResponseDto();
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    response.Status = new Status { Message = e.Message, IsSuccess = false };
                }
            }
            else
            {
                response.Status = new Status
                {
                    Message = $"Unable to delete customer with id = {id}, customer not found",
                    IsSuccess = false
                };
            }
        }

        private bool CustomerExists(long id)
        {
            return (_context.Customers.Any(e => e.Id == id));
        }

        private CustomerModel EntityToDto(Customer entity)
        {
            return new CustomerModel
            {
                Id = entity.Id,
                CompanyName = entity.CompanyName,
                Email = entity.Email,
                Name = entity.Name,
                Phone = entity.Phone
            };
        }
    }
}