using static Backend_Teamwork.src.Entities.User;

namespace Backend_Teamwork.src.DTO
{
    public class UserDTO
    {
        // DTO for creating a new User
        public class UserCreateDto
        {
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public UserRole Role { get; set; } = UserRole.Customer;
            public byte[]? Salt { get; set; }
        }

        // DTO for reading User data
        public class UserReadDto
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public UserRole Role { get; set; }
        }

        // DTO for updating an existing User
        public class UserUpdateDto
        {
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
    }
}
