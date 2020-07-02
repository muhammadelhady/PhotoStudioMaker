using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.DTO
{
    public class ContactRequestDto
    {
        public string Name { get; set; }
        public string BusinessType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PhotoType { get; set; }
        public string NumbersOfPhotos { get; set; }
        public string PhotoBackground { get; set; }
        public IFormFile AttachedFile { get; set; }
    }
}
