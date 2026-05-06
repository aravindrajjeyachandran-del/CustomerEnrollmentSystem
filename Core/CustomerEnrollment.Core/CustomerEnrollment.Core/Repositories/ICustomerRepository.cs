using CustomerEnrollment.Core.Models;
using CustomerEnrollment.Core.Models;
using System.Threading.Tasks;

namespace CustomerEnrollment.Core.Repositories
{
    public interface ICustomerRepository
    {
        int InsertCustomer(CustomerModel model);
        CustomerModel? GetCustomer(int id);
    }
}