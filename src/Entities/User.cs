using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Backend_Teamwork.src.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [
            Required(ErrorMessage = "Name shouldn't be null"),
            MinLength(2, ErrorMessage = "Name should be at at least 2 characters"),
            MaxLength(10, ErrorMessage = "Name shouldn't be more than 10 characters")
        ]
        public string Name { get; set; }

        [RegularExpression(
            @"^\+966[5][0-9]{8}$",
            ErrorMessage = "Phone number should be a valid Saudi phone number"
        )]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Email should be with right format: @gmail.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }    
            public string? Description { set; get; }
        public byte[]? Salt { get; set; }
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
