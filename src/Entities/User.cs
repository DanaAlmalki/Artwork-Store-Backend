using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Backend_Teamwork.src.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [
            MaxLength(30, ErrorMessage = "Name shouldn't be more than 30 characters")
        ]
        public string? Name { get; set; }


        [
            Required(ErrorMessage = "Email shouldn't be null"),
            EmailAddress(ErrorMessage = "Email should be with right format: @gmail.com")
        ]
        public required string Email { get; set; }

        [
            Required(ErrorMessage = "Password shouldn't be null."),
            MinLength(8, ErrorMessage = "Password should be at at least 8 characters")
        ]
        public required string Password { get; set; }
        public string? Description { set; get; }

        [Required(ErrorMessage = "Salt shouldn't be null")]
        public byte[]? Salt { get; set; }

        [Required(ErrorMessage = "Role shouldn't be null")]
        public UserRole Role { get; set; } = UserRole.Customer;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum UserRole
        {
            Admin,
            Customer,
            Artist,
        }
    }
}
