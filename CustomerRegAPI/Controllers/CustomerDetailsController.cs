using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerRegAPI.Models;

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

        /// <summary>
        /// Basic constructor injecting DbContext into controller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="validation"></param>
        public CustomerDetailsController(CustomerRegContext context)
        {
            _context = context;
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
        /// Updates a record currently held in the db for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerDetail"></param>
        /// <returns>ActionResult with status code denoting logics outcome.</returns>
        // PUT: api/CustomerDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDetail(int id, CustomerDetail customerDetail)
        {
            if (id != customerDetail.CustomerID)
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
        /// Creates new customer record .
        /// </summary>
        /// <param name="customerDetail"></param>
        /// <returns>StatusCode denoting the calls outcome.</returns>
        // POST: api/CustomerDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDetail>> PostCustomerDetail(CustomerDetail customerDetail)
        {
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
        /// <returns>Boolean True if matching record found. False if not.</returns>
        private bool CustomerDetailExists(int id)
        {
            return _context.CustomerDetails.Any(e => e.CustomerID == id);
        }
        #endregion
    }
}
