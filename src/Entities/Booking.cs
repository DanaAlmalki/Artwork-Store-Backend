using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        //pending, confirmed, canceled, rejected
        public string Status { get; set; }
        public DateTime BookingDate { get; set; }

        public int WorkshopId { get; set; }
        public int CustomerId { get; set; }
    }
}
