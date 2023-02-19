using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Utilities;
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
			if(file!=null)
			{
				if(!file.CheckFileFormat("image/"))
				{
					throw new Exception();
				}
				if (file.CheckFileSize(100))
				{
					throw new Exception();
				}
			
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
	}
}
