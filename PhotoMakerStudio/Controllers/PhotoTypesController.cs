using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;

namespace PhotoMakerStudio.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PhotoTypesController : Controller
    {
        private readonly IPhotoTypesRepo _photoTypesRepo;

        public PhotoTypesController(IPhotoTypesRepo photoTypesRepo)
        {
            _photoTypesRepo = photoTypesRepo;
        }

        [HttpGet("GetAllPhotoTypes")]
        public async Task<List<PhotoTypes>> GetAllPhotoTypes()
        {
            return await _photoTypesRepo.GetAllPhotoTypes();
        }
        [Authorize]
        [HttpPost("DeletePhotoType")]
        public async Task<IActionResult>DeletePhotoTybe([FromBody]PhotoTypesDto photoTypesDto)
        {
            if (photoTypesDto == null)
                return BadRequest("data object is empty");
            if (await _photoTypesRepo.DeletePhotoTybe(photoTypesDto))
                return Ok("Deleted");
            return BadRequest();
        }

        [Authorize]
        [HttpPost("AddNewPhotoType")]
        public async Task<IActionResult>AddNewPhotoType([FromBody] PhotoTypesDto photoTypesDto )
        {
            if (photoTypesDto == null)
                return BadRequest("data object is empty");

            PhotoTypes photo = await _photoTypesRepo.AddNewPhotoType(photoTypesDto);
            if (photo != null)
                return Ok("New Photo Type Added");
            return BadRequest();
        }
    }
}
