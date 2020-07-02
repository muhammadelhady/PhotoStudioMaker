using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data.Interfaces
{
    public interface IPhotoBackgroundRepo
    {
        public Task<BackgroundTypes> AddNewBackgroundType(BackgroundTypeDto backgroundTypeDto);
        public Task<bool> DeletebackgroundTybe(BackgroundTypeDto backgroundTypeDto);
        public Task<List<BackgroundTypes>> GetAllBackgroundTypes();
    }
}
