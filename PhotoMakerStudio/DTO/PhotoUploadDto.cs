using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.DTO
{
    public class PhotoUploadDto
    {
        public string CategoryName { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
