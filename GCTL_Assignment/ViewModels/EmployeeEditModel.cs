using GCTL_Assignment.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GCTL_Assignment.ViewModels
{
    public class EmployeeEditModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Employee Name is required"), StringLength(100), Display(Name = "Employee name")]
        public string EmployeeName { get; set; } = default!;
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required, StringLength(20)]
        public string Phone { get; set; } = default!;
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? JoinDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage = "Job Title is required"), StringLength(100), Display(Name = "Job Title")]
        public string JobTitle { get; set; } = default!;
        public bool OnAvailable { get; set; }
        public IFormFile? Picture { get; set; } = default!;

    }
}
