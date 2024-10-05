using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Payment? Payment { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Pending,
        Confirmed,
        Canceled,
        Rejected,
    }
}
