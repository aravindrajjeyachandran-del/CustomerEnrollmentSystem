using CustomerEnrollment.Core.Models;
using System.Collections.Concurrent;
using CustomerEnrollment.Core.Models;
using System.Collections.Concurrent;
using System.Threading;

namespace CustomerEnrollment.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConcurrentDictionary<int, CustomerModel> _store = new();
        private int _nextId = 0;

        public int InsertCustomer(CustomerModel model)
        {
            var id = Interlocked.Increment(ref _nextId);
            model.Id = id;
            _store[id] = model;
            return id;
        }

        public CustomerModel? GetCustomer(int id)
        {
            _store.TryGetValue(id, out var found);
            return found;
        }
    }
}