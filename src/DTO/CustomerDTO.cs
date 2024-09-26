namespace Backend_Teamwork.src.DTO
{
    public class CustomerDTO
    {
        // DTO for creating a new customer
        public class CustomerCreateDto
        {
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        // DTO for reading customer data
        public class CustomerReadDto
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
        }

        // DTO for updating an existing customer
        public class CustomerUpdateDto
        {
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
    }
}
