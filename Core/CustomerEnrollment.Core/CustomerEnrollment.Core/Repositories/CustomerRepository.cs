using CustomerEnrollment.Core.Models;
using System.Collections.Concurrent;
using CustomerEnrollment.Core.Models;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CustomerEnrollment.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConcurrentDictionary<int, CustomerModel> _store = new();
        private int _nextId = 0;

        public int InsertCustomer(CustomerModel model)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("usp_EnrollCustomer", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@ProofType", model.ProofType);
            cmd.Parameters.AddWithValue("@ProofNumber", model.ProofNumber);
            cmd.Parameters.AddWithValue("@Address", model.Address);

            var outId = new SqlParameter("@CustomerId", SqlDbType.Int)
            { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outId);

            conn.Open();
            cmd.ExecuteNonQuery();
            return Convert.ToInt32(outId.Value);
        }

        public CustomerModel? GetCustomer(int id)
        {
            new SqlConnection(_connStr)
            _store.TryGetValue(id, out var found);
            return found;
        }
    }
}