using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerRegAPI.Models;
using BusinessLogic.Interfaces;

namespace CustomerRegAPI.Controllers
{
    /// <summary>
    /// Customer API endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly CustomerRegContext _context;
        private readonly IValidation _validation;

        /// <summary>
        /// Basic constructor injecting DbContext and validation into controller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="validation"></param>
        public CustomerDetailsController(CustomerRegContext context,
                                         IValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        /// <summary>
        /// Get all customer records currently held in db.
        /// </summary>
        /// <returns>The list of Customers</returns>
        // GET: api/CustomerDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDetail>>> GetCustomerDetails()
        {
            return await _context.CustomerDetails.ToListAsync();
        }

        /// <summary>
        /// Gets a singular customer detail from the id argument passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Matching record or 404 if no match found</returns>
        // GET: api/CustomerDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetail>> GetCustomerDetail(int id)
        {
            var customerDetail = await _context.CustomerDetails.FindAsync(id);

            if (customerDetail == null)
            {
                return NotFound();
            }

            return customerDetail;
        }

        /// <summary>
        /// Updates a record currently held in the db for the id passed in following successful validation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerDetail"></param>
        /// <returns>ActionResult with status code denoting logics outcome.</returns>
        // PUT: api/CustomerDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDetail(int id, CustomerDetail customerDetail)
        {
            if (id != customerDetail.CustomerID || !ValidateCustomerDetails(customerDetail))
            {
                return BadRequest();
            }

            _context.Entry(customerDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates new customer record ensuring validation before commiting.
        /// </summary>
        /// <param name="customerDetail"></param>
        /// <returns>StatusCode denoting the calls outcome.</returns>
        // POST: api/CustomerDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDetail>> PostCustomerDetail(CustomerDetail customerDetail)
        {
            if (!ValidateCustomerDetails(customerDetail))
            {
                return BadRequest("Invalid customer details submitted.");
            }
            _context.CustomerDetails.Add(customerDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerDetail", new { id = customerDetail.CustomerID }, customerDetail);
        }

        /// <summary>
        /// Deletes customer record matching the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode denoting the outcome</returns>
        // DELETE: api/CustomerDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDetail(int id)
        {
            var customerDetail = await _context.CustomerDetails.FindAsync(id);
            if (customerDetail == null)
            {
                return NotFound();
            }

            _context.CustomerDetails.Remove(customerDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #region Helpers
        /// <summary>
        /// Checks the CustomerDetail table to ensure a matching value is found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CustomerDetailExists(int id)
        {
            return _context.CustomerDetails.Any(e => e.CustomerID == id);
        }

        /// <summary>
        /// Checks both the required and optional values within the model passed into the method.
        /// </summary>
        /// <param name="details"></param>
        /// <returns>Boolean true if all validation passes. Boolean false if not.</returns>
        private bool ValidateCustomerDetails(CustomerDetail details)
        {
            return ValidateRequiredCustomerDetails(details) && ValidateOptionalDetails(details);
        }

        /// <summary>
        /// Checks that the required fields have values passed into the call
        /// </summary>
        /// <param name="details"></param>
        /// <returns>Boolean true if all required values are present and correct. False if not.</returns>
        private bool ValidateRequiredCustomerDetails(CustomerDetail details)
        {
            if (_validation.ValidateString(details.FirstName, 3, 50) &&
                _validation.ValidateString(details.Surname, 3, 50) &&
                _validation.ValidatePolicyReference(details.PolicyReference))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks that the Optional customer details are valid.
        /// </summary>
        /// <param name="details"></param>
        /// <returns>Boolean true if one of the optional values required are present and correct. Boolean false if
        /// checks are not passed.</returns>
        private bool ValidateOptionalDetails(CustomerDetail details)
        {
            if (string.IsNullOrEmpty(details.EmailAddress))
            {
                if (_validation.ValidateAgePlusEighteen(details.DateOfBirth ?? DateTime.Now))
                {
                    return true;
                }
            }
            else
            {
                if (_validation.ValidateEMail(details.EmailAddress))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}