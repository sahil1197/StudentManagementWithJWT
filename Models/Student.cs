using System.ComponentModel.DataAnnotations;

namespace StudentManagementWithJWT.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Percentage is required")]
        public float Percentage { get; set; }

    }
}
