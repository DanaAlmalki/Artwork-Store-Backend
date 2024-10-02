using static Backend_Teamwork.src.Entities.User;

namespace Backend_Teamwork.src.DTO
{
    public class UserDTO
    {
        // DTO for creating a new User (including Artist)
        public class UserCreateDto
        {
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public UserRole Role { get; set; } = UserRole.Customer; // Default to Customer
            public byte[]? Salt { get; set; }

            // Artist-specific properties (optional)
            public string? Description { get; set; } // Nullable, only for Artists
        }

        // DTO for reading User data (including Artist)
        public class UserReadDto
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public UserRole Role { get; set; }

            // Artist-specific properties (optional)
            public string? Description { get; set; } // Nullable, only for Artists
        }

        // DTO for updating an existing User (including Artist)
        public class UserUpdateDto
        {
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }

            // Artist-specific properties (optional)
            public string? Description { get; set; } // Nullable, only for Artists
        }
    }
}
