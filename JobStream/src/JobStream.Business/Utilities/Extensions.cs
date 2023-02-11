using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Utilities
{
	public static class Extensions
	{
		public static async Task<string> CopyFileAsync(this IFormFile file, string root, params string[] folders)
		{
			var path = root;
			var fileName = Guid.NewGuid().ToString() + file.FileName;
			foreach (var folder in folders)
			{
				path = Path.Combine(path, folder);
			}
			path = Path.Combine(path, fileName);
			using(FileStream stream=new(path,FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return fileName;
		}
		public static bool CheckFileSize(this IFormFile file, int MByte)
		{
			return file.Length / (1024 * 1024) > MByte;
		}
		public static bool CheckFileFormat(this IFormFile file, string type)
		{

			return file.ContentType.Contains(type);
		}
	}
}
