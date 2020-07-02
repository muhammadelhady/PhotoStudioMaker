using Microsoft.EntityFrameworkCore;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
    public class PhotoBackroundRepo : IPhotoBackgroundRepo
    {
        private readonly DataContext _dataContext;

        public PhotoBackroundRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<BackgroundTypes> AddNewBackgroundType(BackgroundTypeDto backgroundTypeDto)
        {
            BackgroundTypes background = new BackgroundTypes
            {
                BackgroundType = backgroundTypeDto.BackgroundType
            };
            await _dataContext.BackgroundTypes.AddAsync(background);

            if (await _dataContext.SaveChangesAsync() > 0)
                return background;
            return new BackgroundTypes();

        }

        public async Task<bool> DeletebackgroundTybe(BackgroundTypeDto backgroundTypeDto)
        {
            BackgroundTypes background = await _dataContext.BackgroundTypes.FirstOrDefaultAsync(x=> x.BackgroundType==backgroundTypeDto.BackgroundType);
            _dataContext.BackgroundTypes.Remove(background);
            if (await _dataContext.SaveChangesAsync() > 0)
                return true;
            return false;

        }

        public async Task<List<BackgroundTypes>> GetAllBackgroundTypes()
        {
            return await _dataContext.BackgroundTypes.ToListAsync();
        }
    }
}
