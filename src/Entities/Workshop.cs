namespace Backend_Teamwork.src.Entities
{
    public class Workshop
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; } 
        public bool Availability { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
