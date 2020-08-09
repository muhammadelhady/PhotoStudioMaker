using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
    public class PhotoRepo : IPhotoRepo
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _environment;
        private readonly ICategoryRepo _categoryRepo;


        public PhotoRepo(DataContext dataContext, IWebHostEnvironment environment, ICategoryRepo categoryRepo)
        {
            _dataContext = dataContext;
            _environment = environment;
            _categoryRepo = categoryRepo;
        }

        public async Task<bool> DeleteCatgoryPhotos(CategoryDto categoryDto)
        {
            var photos = await _dataContext.Gallery.Where(x => x.Category.CategoryName == categoryDto.CategoryName).ToListAsync();
            _dataContext.RemoveRange(photos);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePhtoo(DeletePhotoDto deletePhotoDto)
        {
            GalleryPhoto photo = await _dataContext.Gallery.Where(x => x.PhotoId == deletePhotoDto.PhotoId).FirstOrDefaultAsync();

            if (File.Exists(photo.PhotoLocation))
            File.Delete(photo.PhotoLocation);

            _dataContext.Remove(photo);
            if (await _dataContext.SaveChangesAsync() > 0)
                return true;

            return false;

        }

        public async Task<List<GalleryPhoto>> GetAllPhotos()
        {
            return await _dataContext.Gallery.ToListAsync();
        }

        public async Task<List<GalleryPhoto>> GetCategoriesCover()
        {

            List<GalleryPhoto> coverPhotos = new List<GalleryPhoto>();
            List<Category> categories =await _dataContext.Category.ToListAsync();
            for (int i = 0; i < categories.Count;i++ )
                coverPhotos.Add(await _dataContext.Gallery.FirstOrDefaultAsync(x => x.CategoryFK == categories[i].CategoryId));
            return coverPhotos;
            }

        public async Task<List<GalleryPhoto>> GetCategoryPhotos(CategoryIdDto categoryIdDto)
        {

            return await _dataContext.Gallery.Where(x => x.CategoryFK == categoryIdDto.CategoryId).ToListAsync();
        }

        public async Task<List<GalleryPhoto>> GetCategoryPhotosName(CategoryDto categoryDto)
        {
            return await _dataContext.Gallery.Where(x => x.Category.CategoryName == categoryDto.CategoryName).ToListAsync();
        }

        public async Task<GalleryPhoto> SavePhoto(PhotoUploadDto photoUploadDto)
        {

            var uploads = Path.Combine(_environment.WebRootPath, "Gallery");
            if (photoUploadDto.PhotoFile==null)
                return new GalleryPhoto();

            string newPhotoName = DateTime.UtcNow.Ticks.ToString();
            var filePath = Path.Combine(uploads, newPhotoName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photoUploadDto.PhotoFile.CopyToAsync(fileStream);
            }

            Category category = await _categoryRepo.GetCategory(photoUploadDto.CategoryName);

            var galleryPhoto = new GalleryPhoto
            {
                PhotoName = photoUploadDto.PhotoFile.FileName,
                PhotoLocation = filePath,
                PhotoViewCounter = 0,
                Category = category,
                CategoryFK = category.CategoryId
            };
            await _dataContext.Gallery.AddAsync(galleryPhoto);
            if (await _dataContext.SaveChangesAsync()>0)
                return galleryPhoto;

            return new GalleryPhoto();
        }


     
    }
}
