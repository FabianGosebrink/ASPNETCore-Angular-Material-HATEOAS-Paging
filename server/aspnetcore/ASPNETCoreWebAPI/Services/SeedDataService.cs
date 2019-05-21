using ASPNETCoreWebAPI.Entities;
using ASPNETCoreWebAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace ASPNETCoreWebAPI.Services
{
    public class SeedDataService : ISeedDataService
    {
        public async Task Initialize(CustomerDbContext context)
        {
            context.Customers.Add(new Customer() { Name = "Phil Collins", Created = DateTime.Now });
            context.Customers.Add(new Customer() { Name = "Tony Banks", Created = DateTime.Now });
            context.Customers.Add(new Customer() { Name = "Mike Rutherford", Created = DateTime.Now });
            context.Customers.Add(new Customer() { Name = "Chester Thompson", Created = DateTime.Now });
            context.Customers.Add(new Customer() { Name = "Daryl Stuermer", Created = DateTime.Now });
            context.Customers.Add(new Customer() { Name = "Mark Knopfler", Created = DateTime.Now });

            context.Customers.Add(new Customer() { Name = "David Knopfler", Created = DateTime.Now });
            context.Customers.Add(new Customer() { Name = "John Illsley", Created = DateTime.Now });
            context.Customers.Add(new Customer() { Name = "Pick Withers", Created = DateTime.Now });

            await context.SaveChangesAsync();
        }
    }
}
