using CustomerEnrollment.Core.Repositories;
using CustomerEnrollment.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repo;
    private readonly IEncryptionService _enc;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerRepository repo, IEncryptionService enc, ILogger<CustomerController> logger)
    {
        _repo = repo;
        _enc = enc;
        _logger = logger;
    }

    [HttpPost("enroll")]
    public IActionResult Enroll(CustomerRequest req)
    {
        if (req == null)
            return BadRequest("Invalid request");

        try
        {
            // decrypt incoming fields
            var model = new CustomerModel
            {
                Name = _enc.Decrypt(req.Name),
                Mobile = _enc.Decrypt(req.Mobile),
                Email = _enc.Decrypt(req.Email),
                ProofType = _enc.Decrypt(req.ProofType),
                ProofNumber = _enc.Decrypt(req.ProofNumber),
                Address = _enc.Decrypt(req.Address)
            };

            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Name required");

            var id = _repo.InsertCustomer(model);

            return Ok(new { CustomerId = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during enrollment");
            return StatusCode(500, "Something went wrong");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomer(int id)
    {
        try
        {
            var data = _repo.GetCustomer(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fetch failed");
            return StatusCode(500, "Error fetching data");
        }
    }
}