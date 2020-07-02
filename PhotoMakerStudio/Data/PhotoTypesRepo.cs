using Microsoft.EntityFrameworkCore;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
    public class PhotoTypesRepo : IPhotoTypesRepo
    {
        private readonly DataContext _dataContext;

        public PhotoTypesRepo(DataContext dataContext)
        {
           _dataContext = dataContext;
        }
        public async Task<PhotoTypes> AddNewPhotoType(PhotoTypesDto photoTypesDto)
        {
            PhotoTypes photo = new PhotoTypes
            {
                PhotoTybe = photoTypesDto.PhotoType
            };
            await _dataContext.PhotoTypes.AddAsync(photo);
            if (await _dataContext.SaveChangesAsync() > 0)
                return photo;
            return new PhotoTypes();

        }

        public async Task<bool> DeletePhotoTybe(PhotoTypesDto photoTypesDto)
        {
            PhotoTypes photo =await _dataContext.PhotoTypes.FirstOrDefaultAsync(x => x.PhotoTybe == photoTypesDto.PhotoType);
            _dataContext.Remove(photo);
            if (await _dataContext.SaveChangesAsync() > 0)
                return true;
            return false;

        }

        public async Task<List<PhotoTypes>> GetAllPhotoTypes()
        {
            return await _dataContext.PhotoTypes.ToListAsync();
        }
    }
}
