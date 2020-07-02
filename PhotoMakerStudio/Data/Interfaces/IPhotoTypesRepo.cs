using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data.Interfaces
{
   public interface IPhotoTypesRepo
    {
        public Task<PhotoTypes> AddNewPhotoType(PhotoTypesDto photoTypesDto);
        public Task<bool> DeletePhotoTybe(PhotoTypesDto photoTypesDto);
        public Task<List<PhotoTypes>> GetAllPhotoTypes();
    }
}
