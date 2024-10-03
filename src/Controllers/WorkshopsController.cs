using System.Security.Claims;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.workshop;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.WorkshopDTO;

namespace sda_3_online_Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkshopsController : ControllerBase
    {
        private readonly IWorkshopService _workshopService;

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
        // [Authorize(Roles = "Admin")]  // Only Admin
        public async Task<ActionResult<WorkshopReadDTO>> GetWorkshopById([FromRoute] Guid id)
        {
            var workshop = await _workshopService.GetByIdAsync(id);
            if (workshop == null)
            {
                return NotFound($"Workshop with ID {id} not found.");
            }
            return Ok(workshop);
        }

        [HttpGet("page")]
        public async Task<ActionResult<WorkshopReadDTO>> GetWorkShopByPage(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var workshops = await _workshopService.GetAllAsync(paginationOptions);
            if (workshops == null || !workshops.Any())
            {
                return NotFound();
            }
            return Ok(workshops);
        }

        [HttpPost]
        // [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult<WorkshopReadDTO>> CreateWorkshop(
            [FromBody] WorkshopCreateDTO createDto
        )
        {
            // extract user information
            var authenticateClaims = HttpContext.User;
            // get user id from claim
            var userId = authenticateClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;
            // string => guid
            var userGuid = new Guid(userId);

            var workshopCreated = await _workshopService.CreateOneAsync(userGuid, createDto);
            return CreatedAtAction(
                nameof(GetWorkshopById),
                new { id = workshopCreated.Id },
                workshopCreated
            );
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult<bool>> DeleteWorkshop([FromRoute] Guid id)
        {
            var isDeleted = await _workshopService.DeleteOneAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Workshop with ID {id} not found.");
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult<bool>> UpdateWorkshop(
            Guid userId,
            [FromBody] WorkshopUpdateDTO updateDto
        )
        {
            var updateWorkshop = await _workshopService.UpdateOneAsync(userId, updateDto);

            if (!updateWorkshop)
            {
                return NotFound($"Workshop with ID {userId} not found.");
            }
            return NoContent();
        }
    }
}
