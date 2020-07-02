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
    public class BackgroundTypeController : ControllerBase
    {
        private readonly IPhotoBackgroundRepo _backgroundRepo;

        public BackgroundTypeController(IPhotoBackgroundRepo backgroundRepo)
        {
            _backgroundRepo = backgroundRepo;
        }

        [HttpGet("GetAllBackgroundTypes")]
        public async Task <List<BackgroundTypes>> GetAllBackgroundTypes()
        {
            return await _backgroundRepo.GetAllBackgroundTypes();
        }

        [HttpPost("DeleteBackgroundType")]
        public async Task<IActionResult>DeleteBackgroundType([FromBody]BackgroundTypeDto backgroundTypeDto)
        {
            if (backgroundTypeDto == null)
                return BadRequest("data object is empty");

            if (await _backgroundRepo.DeletebackgroundTybe(backgroundTypeDto))
                return Ok("Deleted");
            return BadRequest();
        }

        [HttpPost("AddNewBackgroundType")]
        public async Task <IActionResult>AddNewBackgroundType([FromBody]BackgroundTypeDto backgroundTypeDto)
        {

            if (backgroundTypeDto == null)
                return BadRequest("data object is empty");

            BackgroundTypes background = await _backgroundRepo.AddNewBackgroundType(backgroundTypeDto);
            if (background != null)
                return Ok("Background type added ");

            return BadRequest();
        }
       
    }
}
