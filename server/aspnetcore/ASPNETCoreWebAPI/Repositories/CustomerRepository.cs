using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ASPNETCoreWebAPI.Entities;
using ASPNETCoreWebAPI.Models;
using System.Linq.Dynamic.Core;
using ASPNETCoreWebAPI.Helpers;

namespace ASPNETCoreWebAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public Customer GetSingle(int id)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Customer item)
        {
            _context.Customers.Add(item);
        }

        public void Delete(int id)
        {
            Customer Customer = GetSingle(id);
            _context.Customers.Remove(Customer);
        }

        public Customer Update(int id, Customer item)
        {
            _context.Customers.Update(item);
            return item;
        }

        public IQueryable<Customer> GetAll(QueryParameters queryParameters)
        {
            IQueryable<Customer> _allItems = _context.Customers.OrderBy(queryParameters.OrderBy,
              queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Name.ToString().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _context.Customers.Count();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}