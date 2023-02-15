using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Exceptions
{
	public sealed class CreateUserFailedException:Exception
	{
		public CreateUserFailedException(string message):base(message) 
		{

		}
	}
}
