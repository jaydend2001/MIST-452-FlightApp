using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SP24ClassLibraryDonham
{
    public class AppUser : IdentityUser
    {
        //PK is Id
        //Data Type = GUID (Globally Unique Identifier)
        //GUID Ex) 8-4-4-12

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
        [Required]
        public DateOnly DateOfBirth { get; set; }

        //Int  when perform mathematical functions
        //String else
       // [Required]
       // [DataType(DataType.PhoneNumber)]
        //public string PhoneNumber {  get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public AppUser(string first, string last, DateOnly dob, string phone, string email, string password)
        {
            this.FirstName = first;
            this.LastName = last;
            this.DateOfBirth = dob;
            this.PhoneNumber = phone;

            //Identity User Properties
            this.Email = email;
            this.UserName = email;
            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();
            string hash = hasher.HashPassword(this, password);
            this.PasswordHash = hash;
            this.SecurityStamp = Guid.NewGuid().ToString();
        }

        public AppUser()
        {
            
        }
    }
}
