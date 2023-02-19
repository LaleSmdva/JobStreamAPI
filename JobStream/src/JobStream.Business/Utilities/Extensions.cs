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
