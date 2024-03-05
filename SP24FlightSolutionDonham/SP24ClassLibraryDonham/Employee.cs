using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP24ClassLibraryDonham
{
    public class Employee : AppUser
    {
        [Required]
        public string SSN { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string JobTitle { get; set; }

        public Employee(string ssn, decimal salary, string jobTitle, string first, string last, DateOnly dob, string phone, string email, string password) : base(first, last, dob, phone, email, password)
        {
            this.SSN = ssn;
            this.Salary = salary;
            this.JobTitle = jobTitle;
        }

        public Employee()
        {
            
        }
    }
}
