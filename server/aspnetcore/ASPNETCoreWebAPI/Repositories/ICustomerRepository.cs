using System.Collections.Generic;
using System.Linq;
using ASPNETCoreWebAPI.Entities;
using ASPNETCoreWebAPI.Models;

namespace ASPNETCoreWebAPI.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetSingle(int id);
        void Add(Customer item);
        void Delete(int id);
        Customer Update(int id, Customer item);
        IQueryable<Customer> GetAll(QueryParameters queryParameters);

        int Count();

        bool Save();
    }
}
