﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Exceptions
{
    public class RepeatedChoiceException:Exception
    {
        public RepeatedChoiceException(string message):base(message)
        {

        }
    }
}
