using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure.Repositries
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> SaveImageSrc= new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);
            if (!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }
            foreach (var file in files) {
                if (file.Length > 0) {
                    var ImageName = file.FileName;
                    var ImageSrc = $"Images/{src}/{ImageName} ";
                    var filePath = Path.Combine(ImageDirectory, ImageName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(ImageSrc);

                }
            }
            return SaveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var filePath = info.PhysicalPath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
