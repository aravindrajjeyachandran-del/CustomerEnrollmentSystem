
namespace CustomerEnrollment.Core.Models
{
    public class CustomerRequest
    {
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? ProofType { get; set; }
        public string? ProofNumber { get; set; }
        public string? Address { get; set; }
    }
}