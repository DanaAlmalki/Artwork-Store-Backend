using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.Entities;

namespace Backend_Teamwork.src.DTO
{
    public class BookingDTO
    {
        public class BookingCreateDto
        {
            public Guid WorkshopId { get; set; }
        }

        public class BookingReadDto
        {
            public Guid Id { get; set; }
            public Status Status { get; set; }
            public DateTime CreatedAt { get; set; }
            public Workshop Workshop { get; set; }
            public Guid UserId { get; set; }
        }
    }
}
