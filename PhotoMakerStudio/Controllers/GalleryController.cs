using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PhotoMakerStudio.Data;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;

namespace PhotoMakerStudio.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IPhotoRepo _photoRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IWebHostEnvironment _environment;

        public GalleryController(IPhotoRepo photoRepo, ICategoryRepo categoryRepo, IWebHostEnvironment environment)
        {
           _photoRepo = photoRepo;
            _categoryRepo = categoryRepo;
            _environment = environment;
        }


      

        [HttpPost("upload")]
        public async Task<IActionResult> Register([FromForm] PhotoUploadDto photoUploadDto)
        {
            if (await _photoRepo.SavePhoto(photoUploadDto)!=null)
                return StatusCode(201);
            return BadRequest();
        }


        [HttpPost("DeletePhoto")]
        public async Task<IActionResult> DeletePhoto([FromBody]DeletePhotoDto deletePhotoDto)
        {
            if (await _photoRepo.DeletePhtoo(deletePhotoDto))
                return Ok("Deleted");
            return BadRequest();
        }

        [HttpGet("GetAllPhotos")]
        public async Task<List<GalleryPhoto>>GetAllPhotos()
        {
           return await _photoRepo.GetAllPhotos();
        }
    }
}
