﻿using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
   public interface IPhotoRepo
    {
        public Task<GalleryPhoto> SavePhoto(PhotoUploadDto photoUploadDto);
        public Task<bool> DeletePhtoo(DeletePhotoDto deletePhotoDto);

        public Task<List<GalleryPhoto>> GetAllPhotos();
      

    }
}
