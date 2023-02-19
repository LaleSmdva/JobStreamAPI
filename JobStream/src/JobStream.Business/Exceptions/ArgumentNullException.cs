using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Exceptions;

public class ArgumentNullException:Exception
{
	public ArgumentNullException(string message):base(message)
	{

	}
}
