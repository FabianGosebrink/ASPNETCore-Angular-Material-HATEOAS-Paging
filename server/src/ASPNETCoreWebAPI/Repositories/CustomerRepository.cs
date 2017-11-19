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
        private readonly ConcurrentDictionary<int, Customer> _storage = new ConcurrentDictionary<int, Customer>();

        public Customer GetSingle(int id)
        {
            Customer customer;
            return _storage.TryGetValue(id, out customer) ? customer : null;
        }

        public void Add(Customer item)
        {
            item.Id = !_storage.Values.Any() ? 1 : _storage.Values.Max(x => x.Id) + 1;

            if (!_storage.TryAdd(item.Id, item))
            {
                throw new Exception("Item could not be added");
            }
        }

        public void Delete(int id)
        {
            Customer customer;
            if (!_storage.TryRemove(id, out customer))
            {
                throw new Exception("Item could not be removed");
            }
        }

        public Customer Update(int id, Customer item)
        {
            _storage.TryUpdate(id, item, GetSingle(id));
            return item;
        }

        public IQueryable<Customer> GetAll(QueryParameters queryParameters)
        {
            IQueryable<Customer> _allItems = _storage.Values.AsQueryable().OrderBy(queryParameters.OrderBy,
               queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Name.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _storage.Count;
        }

        public bool Save()
        {
            // To keep interface consistent with Controllers, Tests & EF Interfaces
            return true;
        }
    }
}