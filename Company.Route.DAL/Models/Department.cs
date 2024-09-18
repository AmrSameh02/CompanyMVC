using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route.DAL.Models
{
    public class Department:BaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

    }
}
