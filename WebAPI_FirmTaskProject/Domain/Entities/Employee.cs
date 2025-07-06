using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Department { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.UtcNow;
        public DateTime EmploymentDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
    }
}
