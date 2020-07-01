using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.DTO
{
    public class PartnerDto
    {
        public string PartnerName { get; set; }
        public string PartnerWebsite { get; set; }
        public IFormFile PartnerLogo { get; set; }
    }
}
