using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Utilities;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.HelperServices.Implementations
{
	public class FileService : IFileService
	{
        

        public async  Task<string> CopyFileAsync(IFormFile file, string root, params string[] folders)
		{
			if(file==null)
			{
				throw new NotFoundException("File not found");
			}

            //if (!file.CheckFileFormat("image/"))
            //{
            //    throw new FileFormatException("Choose an image");
            //}
            if (file.CheckFileSize(2))
            {
                throw new FileSizeException("File size has exceeded its max limit of 1 MB ");
            }

            var path = root;
			var fileName = Guid.NewGuid().ToString() + file.FileName;
			foreach (var folder in folders)
			{
				path = Path.Combine(path, folder);
			}
			path = Path.Combine(path, fileName);
			using (FileStream stream = new(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return fileName;
		}

        public async Task DeleteFileAsync(string file, string root, params string[] folders)
        {
            var path = root;
            foreach (var folder in folders)
            {
                path = Path.Combine(path, folder);
            }
            path = Path.Combine(path, file);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
