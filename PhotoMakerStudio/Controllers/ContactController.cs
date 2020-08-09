using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoMakerStudio.Data;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;

namespace PhotoMakerStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactRequestRepo _contactRequestRepo;

        public ContactController(IContactRequestRepo contactRequestRepo)
        {
            _contactRequestRepo = contactRequestRepo;
        }

        [Authorize]
        [HttpGet("GetAllRequests")]
        public async Task<List<ContactRequest>>GetAllRequests()
        {
            return await _contactRequestRepo.GetAllRequests();
        }

        [HttpPost("NewRequest")]
        public async Task<IActionResult>NewRequest([FromForm]ContactRequestDto contactRequestDto)
        {
            if (contactRequestDto == null)
                return BadRequest("data object is empty");
            ContactRequest request = await _contactRequestRepo.AddNewContactRequest(contactRequestDto);
            if (request != null)
                return StatusCode(201);
            return BadRequest();
        }
        [Authorize]
        [HttpPost("DeleteRequest")]
        public async Task<IActionResult>DeleteRequest(DeleteContactRequestDto deleteRequestDto)
        {
            if (await _contactRequestRepo.DeleteContactRequest(deleteRequestDto))
                return Ok("Deleted");
            return BadRequest();
        }
      
    }
}
