using System.ComponentModel.DataAnnotations;

namespace model {
    public class Users
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(40 ,ErrorMessage = "Name can't be no longer than 40 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20 ,MinimumLength = 8 ,ErrorMessage = "Password must be at least 8 characters and not more than 255 characters")]
        public required string Password { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserRoles { get; set; }
    }
}
