using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.HelperServices.Interfaces
{
	public interface IFileService
	{
		Task<string> CopyFileAsync(IFormFile file, string root, params string[] folders);
		Task DeleteFileAsync(string file, string root, params string[] folders);
	}
}
