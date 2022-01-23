using System.ComponentModel.DataAnnotations;

namespace CustomerRegAPI.Models
{
    /// <summary>
    /// Model containing all customer record detailing
    /// </summary>
    public partial class CustomerDetail
    {
        /// <summary>
        /// Primary key for the customer record
        /// </summary>
        [Key]
        public int CustomerID { get; set; }
        /// <summary>
        /// The customers first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Customers Surname.
        /// </summary>
        [Required]
        public string Surname { get; set; } = string.Empty;
        /// <summary>
        /// The Policy Reference attached to the customer containing 2 alpha chars, a dash and 6 numeric values.
        /// </summary>
        [Required]
        public string PolicyReference { get; set; } = string.Empty;
        /// <summary>
        /// Customer date of birth. (Optional detail)
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Email address for the customer (Optional detail)
        /// </summary>
        public string? EmailAddress { get; set; }
    }
}
