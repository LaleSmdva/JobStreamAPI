﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Exceptions
{
	public sealed class NotFoundException:Exception
	{
		public NotFoundException(string message):base(message)
		{

		}
	}
}
