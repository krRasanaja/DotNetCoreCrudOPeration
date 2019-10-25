using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Departments
    {
        [Key]
        public int DepartmentID { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
    }
}
