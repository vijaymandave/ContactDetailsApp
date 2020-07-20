using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ContactDetail
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(80)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string ContactStatus { get; set; }

        private bool status;

        public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                ContactStatus = status ? "Active" : "Inactive";
            }
        }
        
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public ContactDetail()
        {
            Id = Guid.NewGuid();
            Status = true;
        }
    }
}