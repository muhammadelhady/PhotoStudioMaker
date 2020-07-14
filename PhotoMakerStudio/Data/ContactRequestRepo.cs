using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
    public class ContactRequestRepo : IContactRequestRepo
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _environment;

        public ContactRequestRepo(DataContext dataContext, IWebHostEnvironment environment)
        {
            _dataContext = dataContext;
           _environment = environment;
        }
        public async Task<ContactRequest> AddNewContactRequest([FromForm]ContactRequestDto contactRequestDto )
        {
            var uploads = Path.Combine(_environment.WebRootPath, "ContactRequestsFiles");
            string filePath="";
            if (contactRequestDto.AttachedFile!=null) {
                string newAttachedFileName = DateTime.UtcNow.Ticks.ToString();
                 filePath = Path.Combine(uploads, newAttachedFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await contactRequestDto.AttachedFile.CopyToAsync(fileStream);
                }
            }

           if (filePath=="")
                filePath = "no file attached";
            

            ContactRequest request = new ContactRequest
            {
                Name=contactRequestDto.Name,
                BusinessType=contactRequestDto.BusinessType,
                Phone=contactRequestDto.Phone,
                Email=contactRequestDto.Email,
                PhotoType=contactRequestDto.PhotoType,
                NumbersOfPhotos=contactRequestDto.NumbersOfPhotos,
                PhotoBackground=contactRequestDto.PhotoBackground,
                AttachedFileLocation=filePath
            };

            await _dataContext.ContactRequests.AddAsync(request);
            if (await _dataContext.SaveChangesAsync() > 0)
                return request;

            return new ContactRequest();
        }

        public async Task<bool> DeleteContactRequest(DeleteContactRequestDto deleteContactRequestDto)
        {
            ContactRequest request = await _dataContext.ContactRequests.FirstOrDefaultAsync(x => x.Id == deleteContactRequestDto.Id);

            if (File.Exists(request.AttachedFileLocation))
                File.Delete(request.AttachedFileLocation);

            _dataContext.ContactRequests.Remove(request);
            if (await _dataContext.SaveChangesAsync() >= 0)
                return true;
            return false;
        }

        public async Task<List<ContactRequest>> GetAllRequests()
        {
            return await _dataContext.ContactRequests.ToListAsync();
        }
    }
}
