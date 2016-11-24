using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Customers
{
    public class CustomerDTO
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime? BirthDate { get; set; }

    }
}