namespace Backend_Teamwork.src.DTO
{
    public class PaymentDTO
    {
             public class PaymentCreateDTO
            {
            public string PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public DateTime CreatedAt { get; set; }
            }
        public class PaymentReadDTO
       {
           public Guid Id { get; set; }
            public string PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public DateTime CreatedAt { get; set; }
        }
        public class PaymentUpdateDTO
        {
            public string PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}