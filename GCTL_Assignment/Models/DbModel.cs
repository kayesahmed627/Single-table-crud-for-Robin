using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GCTL_Assignment.Models
{
    public enum Gender { Male = 1, Female }
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Employee Name is required"), StringLength(100), Display(Name = "First name")]
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
        [Required, StringLength(100)]
        public string Picture { get; set; } = default!;
    }
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, EmployeeName="Abu Hasan", Email = "hasan123@gmail.com", Phone = "01322556699", Gender = Gender.Male, JoinDate = DateTime.Now.AddYears(-19), Salary = 25000, JobTitle = "Developer", OnAvailable = false, Picture = "1.jpg" }
                );
        }
    }
}
