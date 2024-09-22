using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backend_Teamwork.src.Controllers;
using Backend_Teamwork.src.Entities;
using Backend.src.Entities;
using Microsoft.AspNetCore.Mvc;
using sda_3_online_Backend_Teamwork.src.Controllers;
using sda_3_online_Backend_Teamwork.src.Entities;

namespace Backend.src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class BookingsController : ControllerBase
    {
        private static readonly List<Booking> _bookings = new List<Booking>()
        {
            new Booking
            {
                Id = 1,
                CustomerId = 1,
                WorkshopId = 1,
                Status = "pending",
                BookingDate = DateTime.Now,
            },
            new Booking
            {
                Id = 2,
                CustomerId = 2,
                WorkshopId = 1,
                Status = "pending",
                BookingDate = DateTime.Now,
            },
            new Booking
            {
                Id = 3,
                CustomerId = 3,
                WorkshopId = 1,
                Status = "pending",
                BookingDate = DateTime.Now,
            },
        };
        private readonly WorkshopController? _workshopsController;
        private readonly CustomersController? _customersController;
        private readonly PaymentController? _paymentController;

        [HttpGet]
        public ActionResult GetBookings()
        {
            if (_bookings.Count == 0)
            {
                return NotFound();
            }
            return Ok(_bookings);
        }

        [HttpGet("{id}")]
        public ActionResult GetBookingById(int id)
        {
            Booking? foundBooking = _bookings.FirstOrDefault(b => b.Id == id);
            if (foundBooking == null)
            {
                return NotFound();
            }
            return Ok(foundBooking);
        }

        [HttpGet("customerId/{customerId}")]
        public ActionResult GetBookingsByCustomerId(int customerId)
        {
            List<Booking> foundBookings = _bookings.FindAll(b => b.CustomerId == customerId);
            if (foundBookings.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBookings);
        }

        [HttpGet("status/{status}")]
        public ActionResult GetBookingsByStatus(string status)
        {
            List<Booking> foundBookings = _bookings.FindAll(b => b.Status == status);
            if (foundBookings.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBookings);
        }

        [HttpGet("customerId/{customerId}/status/{status}")]
        public ActionResult GetBookingsByCustomerIdAndStatus(int customerId, string status)
        {
            List<Booking> foundBookings = _bookings.FindAll(b =>
                b.CustomerId == customerId && b.Status == status
            );
            if (foundBookings.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBookings);
        }

        [HttpGet("page/{pageNumber}/{pageSize}")]
        public ActionResult GetBookingsByPage(int pageNumber, int pageSize)
        {
            if (_bookings.Count == 0)
            {
                return NotFound();
            }
            return Ok(_bookings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        [HttpGet("page/{pageNumber}/{pageSize}/customerId/{customerId}")]
        public ActionResult GetBookingsByPageAndCustomerId(
            int customerId,
            int pageNumber,
            int pageSize
        )
        {
            List<Booking> foundBookings = _bookings.FindAll(b => b.CustomerId == customerId);
            if (foundBookings.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBookings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        [HttpPost]
        public ActionResult AddBooking(Booking booking)
        {
            //1. check the workshop and customer
            var customerActionResult = _customersController?.GetCustomerById(booking.CustomerId);
            var workshopActionResult = _workshopsController?.GetWorkshopById(booking.WorkshopId);
            if (
                (Int32)
                    customerActionResult
                        .GetType()
                        .GetProperty("StatusCode")
                        ?.GetValue(customerActionResult) != 200
                || (Int32)
                    workshopActionResult
                        .GetType()
                        .GetProperty("StatusCode")
                        ?.GetValue(workshopActionResult) != 200
            )
            {
                return NotFound();
            }

            //2. check the workshop availability
            //3. check if the customer enrolled this workshop before
            //4. check if the customer has a workshop at the same time
            Workshop? workshop = (Workshop)
                workshopActionResult.GetType().GetProperty("Value")?.GetValue(workshopActionResult); //DAR

            Booking? foundBooking = _bookings.Find(b =>
                b.CustomerId == booking.CustomerId && b.WorkshopId == booking.WorkshopId
            );

            var workshopsActionResult = _workshopsController.GetWorkshops();
            List<Workshop> workshops =
                (List<Workshop>)
                    workshopsActionResult
                        .GetType()
                        .GetProperty("Value")
                        ?.GetValue(workshopsActionResult);
            Workshop foundWorkshop = workshops.Find(w =>
                (w.StartTime == workshop.StartTime && w.EndTime == workshop.EndTime)
                || (w.EndTime > workshop.StartTime && w.StartTime < workshop.StartTime)
                || (w.StartTime < workshop.EndTime && w.StartTime > workshop.StartTime)
            );
            bool isFound = _bookings.Any(b =>
                b.CustomerId == booking.CustomerId && b.WorkshopId == foundWorkshop.Id
            );

            if (!workshop.Availability || foundBooking != null || isFound)
            {
                return BadRequest();
            }
            booking.Status = "pending";
            booking.BookingDate = DateTime.Now;
            return Created("Booking has been added successfully", booking);
        }

        //confirm booking (after payment)
        [HttpPatch("confirm/{id}")]
        public ActionResult ConfirmBooking(int id)
        {
            Booking? foundBooking = _bookings.Find(b => b.Id == id);
            if (foundBooking == null)
            {
                return NotFound();
            }
            //check the payment and workshop availability
            var workshopActionResult = _workshopsController?.GetWorkshopById(
                foundBooking.WorkshopId
            );

            Workshop workshop = (Workshop)
                workshopActionResult.GetType().GetProperty("Value")?.GetValue(workshopActionResult); //DAR

            var paymentsActionResult = _paymentController?.GetPayments();
            List<Payment> payments =
                (List<Payment>)
                    paymentsActionResult
                        .GetType()
                        .GetProperty("Value")
                        ?.GetValue(paymentsActionResult);
            //bool isFound=payments.Any(p=>p.BookingId==foundBooking.Id);
            bool isFound = true;

            if (!workshop.Availability || isFound)
            {
                return BadRequest();
            }
            foundBooking.Status = "confirmed";
            return Ok(foundBooking);
        }

        //cancel booking
        [HttpPatch("cancel/{id}")]
        public ActionResult CancelBooking(int id)
        {
            Booking? foundBooking = _bookings.Find(b => b.Id == id);
            if (foundBooking == null)
            {
                return NotFound();
            }
            if (foundBooking.Status != "pending")
            {
                return BadRequest();
            }
            foundBooking.Status = "canceled";
            return Ok(foundBooking);
        }

        //reject bookings
        [HttpPatch("reject")]
        public ActionResult RejectBookings()
        {
            if (_bookings.Count == 0)
            {
                return NotFound();
            }

            //check the workshop enddate
            foreach (Booking booking in _bookings)
            {
                if (
                    booking.Status == "pending" /*&& booking.Workshop.Enddate<Datetime.Now*/ //NR
                )
                {
                    booking.Status = "rejected";
                }
            }
            return Ok();
        }
    }
}
