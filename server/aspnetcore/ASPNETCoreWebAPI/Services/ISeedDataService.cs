using System.Threading.Tasks;
using ASPNETCoreWebAPI.Repositories;

namespace ASPNETCoreWebAPI.Services
{
    public interface ISeedDataService
    {
        Task Initialize(CustomerDbContext context);
    }
}
