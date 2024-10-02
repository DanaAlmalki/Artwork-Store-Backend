using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.workshop;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.WorkshopDTO;

namespace sda_3_online_Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkshopsController : ControllerBase
    {
        private readonly IWorkshopService _workshopService;
        public static List<Workshop> workshops = new List<Workshop>()
        {
            new Workshop
            {
                // Id = 1,
                Name = "Artify-Art Workshop",
                Location = "Jeddah",
                StartTime = new DateTime(2024, 9, 20, 10, 0, 0),
                Price = 50.0m,
                Availability = true,
            },
            new Workshop
            {
                // Id = 2,
                Name = "Artify-Photography Workshop",
                Location = "Riyadh",
                StartTime = new DateTime(2024, 10, 1, 14, 0, 0),
                Price = 75.0m,
                Availability = true,
            },
            new Workshop
            {
                // Id = 3,
                Name = "Artify-Painting Workshop",
                Location = "Dammam",
                StartTime = new DateTime(2024, 9, 25, 9, 0, 0),
                Price = 60.0m,
                Availability = false,
            },
        };

        public WorkshopsController(IWorkshopService service)
        {
            _workshopService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkshopReadDTO>>> GetWorkshop()
        {
            var workshops = await _workshopService.GetAllAsync();
            if (workshops == null || !workshops.Any())
            {
                return NotFound();
            }
            return Ok(workshops);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkshopReadDTO>> GetWorkshopById(Guid id)
        {
            var workshop = await _workshopService.GetByIdAsync(id);
            if (workshop == null)
            {
                return NotFound($"Workshop with ID {id} not found.");
            }
            return Ok(workshop);
        }

        [HttpGet("page/{pageNumber}/{pageSize}")]
        public ActionResult GetWorkShopByPage(int pageNumber, int pageSize)
        {
            var pagedWorkshop = workshops.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Ok(pagedWorkshop);
        }

        [HttpPost]
        public async Task<ActionResult<WorkshopReadDTO>> SignUp(WorkshopCreateDTO createDto)
        {
            var workshopCreated = await _workshopService.CreateOneAsync(createDto);
            return CreatedAtAction(
                nameof(GetWorkshopById),
                new { id = workshopCreated.Id },
                workshopCreated
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteWorkshop(Guid id)
        {
            var isDeleted = await _workshopService.DeleteOneAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Workshop with ID {id} not found.");
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateWorkshop(Guid id, WorkshopUpdateDTO updateDto)
        {
            var updateWorkshop = await _workshopService.UpdateOneAsync(id, updateDto);

            if (!updateWorkshop)
            {
                return NotFound($"Workshop with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
