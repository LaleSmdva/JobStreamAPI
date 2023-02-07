using JobStream.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities
{
	public class SendMessage : IEntity
	{
		public int Id { get; set; }
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Header { get; set; }
		public string Message { get; set; }
	}
}
