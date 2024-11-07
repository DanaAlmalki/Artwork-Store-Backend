using System.ComponentModel.DataAnnotations;
using Backend_Teamwork.src.Utils;
using static Backend_Teamwork.src.Entities.User;

namespace Backend_Teamwork.src.DTO
{
    public class UserDTO
    {
        // DTO for creating a new User (including Artist)
        public class UserCreateDto
        {
            [
                MaxLength(30, ErrorMessage = "Name shouldn't be more than 10 characters")
            ]
            public string? Name { get; set; }


            [
                Required(ErrorMessage = "Email shouldn't be null"),
                EmailAddress(ErrorMessage = "Email should be with right format: @gmail.com")
            ]
            public required string Email { get; set; }

            [
                Required(ErrorMessage = "Password shouldn't be null"),
                MinLength(8, ErrorMessage = "Password should be at at least 8 characters")
            ]
            public required string Password { get; set; }

            [Required(ErrorMessage = "Role shouldn't be null")]
            public UserRole Role { get; set; } = UserRole.Customer; // Default to Customer

            // Artist-specific properties (optional)
            public string? Description { get; set; } // Nullable, only for Artists
        }

        public class UserSigninDto
        {
            [
                Required(ErrorMessage = "Email shouldn't be null"),
                EmailAddress(ErrorMessage = "Email should be with right format: @gmail.com")
            ]
            public required string Email { get; set; }

            [Required(ErrorMessage = "Password shouldn't be null")]
            public required string Password { get; set; }
        }

        // DTO for reading User data (including Artist)
        public class UserReadDto
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public UserRole Role { get; set; }

            // Artist-specific properties (optional)
            public string? Description { get; set; } // Nullable, only for Artists
        }

        // DTO for updating an existing User (including Artist)
        [AtLeastOneRequired(ErrorMessage = "At least one property must be updated.")]
        public class UserUpdateDto
        {
            [
                MaxLength(30, ErrorMessage = "Name shouldn't be more than 10 characters")
            ]
            public string? Name { get; set; }

            [EmailAddress(ErrorMessage = "Email should be with right format: @gmail.com")]
            public string? Email { get; set; }

            [MinLength(8, ErrorMessage = "Password should be at at least 8 characters")]
            public string? Password { get; set; }

            [MinLength(2, ErrorMessage = "Description should be at at least 2 characters")]
            public string? Description { get; set; }
        }
    }
}
