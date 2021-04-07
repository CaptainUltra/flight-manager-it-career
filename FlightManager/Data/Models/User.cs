using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; }
        [Required]
        [PersonalData]
        public string LastName { get; set; }
        [Required]
        [PersonalData]
        public string PersonalNo { get; set; }
        [Required]
        [PersonalData]
        public string Address { get; set; }
    }
}
