using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class Comment
	{
		public int id { get; set; }
		public DateTime createdAt { get; set; }
		public string body { get; set; }
		public int userId { get; set; }
		public int PostId { get; set; }
		public int likes { get; set; }

		public override string ToString()
		{
			return string.Format($"{body}");
		}
	}
}
