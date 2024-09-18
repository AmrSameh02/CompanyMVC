using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route.DAL.Models
{
    public class Employee:BaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        [Range(19,60,ErrorMessage ="Age must be between 19 and 60")]
        public int? Age { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage ="Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DataType(DataType.EmailAddress)]
        
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
