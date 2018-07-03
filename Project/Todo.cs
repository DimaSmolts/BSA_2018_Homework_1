using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class Todo
	{
		public int id { get; set; }
		public DateTime createdAt { get; set; }
		public string name { get; set; }
		public bool isCompleted { get; set; }
		public int userId { get; set; }
	}
}
