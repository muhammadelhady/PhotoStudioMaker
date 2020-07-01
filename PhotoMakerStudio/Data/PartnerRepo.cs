using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data.Interfaces
{
    public class PartnerRepo : IPartnerRepo
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _environment;

        public PartnerRepo( DataContext dataContext,IWebHostEnvironment environment)
        {
             _dataContext = dataContext;
            _environment = environment;
        }
        public async Task<Partner> AddPartner(PartnerDto partnerDto)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "PartnerLogos");
            if (partnerDto.PartnerLogo.Length <= 0)
                return new Partner();

            string newLogoName = DateTime.UtcNow.Ticks.ToString();
            var filePath = Path.Combine(uploads, newLogoName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await partnerDto.PartnerLogo.CopyToAsync(fileStream);
            }

            Partner partner = new Partner
            {
                PartnerName = partnerDto.PartnerName,
                PartnerWebsite=partnerDto.PartnerWebsite,
                LogoLocation=filePath
                
            };
            await _dataContext.Partners.AddAsync(partner);

            if (await _dataContext.SaveChangesAsync()>0)
                    return partner;
            return new Partner();
            
        }

        public async Task<bool> DeletePartner(DeletePartnerDto deletePartnerDto)
        {
            Partner partner = await _dataContext.Partners.FirstOrDefaultAsync(x => x.PartnerId == deletePartnerDto.PartnerID);

            if (File.Exists(partner.LogoLocation))
                File.Delete(partner.LogoLocation);

            _dataContext.Partners.Remove(partner);
            if (await _dataContext.SaveChangesAsync() > 0)
                return true;

            return false;

        }

        public async Task< List<Partner>> GetPartners()
        {
             return await _dataContext.Partners.ToListAsync();
        }
    }
}
