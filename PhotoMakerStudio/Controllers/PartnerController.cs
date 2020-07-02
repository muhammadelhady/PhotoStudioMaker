using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;

namespace PhotoMakerStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : Controller
    {
        private readonly IPartnerRepo _partnerRepo;

        public PartnerController(IPartnerRepo partnerRepo)
        {
            _partnerRepo = partnerRepo;
        }

        [HttpGet("GetAllPartners")]
        public async Task<List<Partner>>GetAllPartners()
        {
            return await _partnerRepo.GetPartners();
        }

        [HttpPost("DeletePartner")]
        public async Task<IActionResult>DeletePartner(DeletePartnerDto deletePartnerDto)
        {
            if (deletePartnerDto == null)
                return BadRequest("data object is empty");

            if (await _partnerRepo.DeletePartner(deletePartnerDto))
                return Ok("Deleted");
            return BadRequest();
               
        }


        [HttpPost("AddNewPartner")]
        public async Task<IActionResult> AddNewPartner([FromForm]PartnerDto partnerDto)
        {
            if (partnerDto == null)
                return BadRequest("data object is empty");

            if (partnerDto == null)
                return BadRequest();

            if (await _partnerRepo.AddPartner(partnerDto) != null)
                return StatusCode(201);
            return BadRequest();
        }
      
    }
}
