using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Backend_Teamwork.src.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
