using Backend_Teamwork.src.Entities;
using Microsoft.AspNetCore.Mvc;


namespace sda_3_online_Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkshopController : ControllerBase
    {
        public static List<Workshop> workshops = new List<Workshop>()
        {
            new Workshop
            {
                Id = 1,
                Name = "Artify-Art Workshop",
                Location = "Jeddah",
                StartTime = new DateTime(2024, 9, 20, 10, 0, 0),
                Price = 50.0m,
                Availability = true,
            },
            new Workshop
            {
                Id = 2,
                Name = "Artify-Photography Workshop",
                Location = "Riyadh",
                StartTime = new DateTime(2024, 10, 1, 14, 0, 0),
                Price = 75.0m,
                Availability = true,
            },
            new Workshop
            {
                Id = 3,
                Name = "Artify-Painting Workshop",
                Location = "Dammam",
                StartTime = new DateTime(2024, 9, 25, 9, 0, 0),
                Price = 60.0m,
                Availability = false,
            },
        };

        [HttpGet]
        public ActionResult GetWorkshops()
        {
            if (workshops.Count == 0)
            {
                return NotFound();
            }
            return Ok(workshops);
        }

        [HttpGet("{id}")]
        public ActionResult GetWorkshopById(int id)
        {
            Workshop? foundWorkshop = workshops.FirstOrDefault(p => p.Id == id);
            if (foundWorkshop == null)
            {
                return NotFound();
            }
            return Ok(foundWorkshop);
        }

        [HttpGet("page/{pageNumber}/{pageSize}")]
        public ActionResult GetWorkShopByPage(int pageNumber, int pageSize)
        {
            var pagedWorkshop = workshops.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Ok(pagedWorkshop);
        }

        [HttpPost]
        public ActionResult CreateWorkshop(Workshop newWorkshop)
        {
            workshops.Add(newWorkshop);
            return CreatedAtAction(
                nameof(GetWorkshopById),
                new { id = newWorkshop.Id },
                newWorkshop
            );
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteWorkshop(int id)
        {
            Workshop? foundworkshop = workshops.FirstOrDefault(p => p.Id == id);
            if (foundworkshop == null)
            {
                return NotFound();
            }
            workshops.Remove(foundworkshop);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateWorkshop(int id, Workshop updatedWorkshop)
        {
            Workshop? existingWorkshop = workshops.FirstOrDefault(p => p.Id == id);

            if (existingWorkshop == null)
            {
                return NotFound();
            }

            existingWorkshop.Name = updatedWorkshop.Name;
            existingWorkshop.Location = updatedWorkshop.Location;
            existingWorkshop.StartTime = updatedWorkshop.StartTime;
            existingWorkshop.Price = updatedWorkshop.Price;
            existingWorkshop.Availability = updatedWorkshop.Availability;
            existingWorkshop.Capacity = updatedWorkshop.Capacity;

            return NoContent();
        }
    }
}
