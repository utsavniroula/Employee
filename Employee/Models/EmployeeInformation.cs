using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class EmployeeInformation
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(0, 50000)]
        public int Salary { get; set; }
    }
}
