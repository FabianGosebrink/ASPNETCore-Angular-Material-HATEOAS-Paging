using ASPNETCoreWebAPI.Entities;
using ASPNETCoreWebAPI.Repositories;
using System;

namespace ASPNETCoreWebAPI.Services
{
    public class SeedDataService : ISeedDataService
    {
        ICustomerRepository _repository;

        public SeedDataService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public void EnsureSeedData()
        {
            _repository.Add(new Customer() { Id = 1, Name = "Phil Collins", Created = DateTime.Now });
            _repository.Add(new Customer() { Id = 2, Name = "Tony Banks", Created = DateTime.Now });
            _repository.Add(new Customer() { Id = 2, Name = "Mike Rutherford", Created = DateTime.Now });
            _repository.Add(new Customer() { Id = 2, Name = "Chester Thompson", Created = DateTime.Now });
            _repository.Add(new Customer() { Id = 2, Name = "Daryl Stuermer", Created = DateTime.Now });
            _repository.Add(new Customer() { Id = 2, Name = "Mark Knopfler", Created = DateTime.Now });

            _repository.Add(new Customer() { Id = 2, Name = "David Knopfler", Created = DateTime.Now });
            _repository.Add(new Customer() { Id = 2, Name = "John Illsley", Created = DateTime.Now });
            _repository.Add(new Customer() { Id = 2, Name = "Pick Withers", Created = DateTime.Now });
            
        }
    }
}
