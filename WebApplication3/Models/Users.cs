using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Users
    {
        [Required]
        public int Id { get; set; }
        
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(15)]
        [Required]
        public string Fname { get; set; }
        
        [StringLength(15)]
        [Required]
        public string Lname { get; set; }
        
        [RegularExpression("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$")]
        [Required]
        public string Phone { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        [Url]
        public String ImageUrl { get; set; }
        
        [Required]
        public DateTime Dob { get; set; }
        
        [Required]
        public string Gender { get; set; }

    }
}
