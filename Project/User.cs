using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class User
	{
		public int id { get; set; }
		public DateTime createdAt { get; set; }
		public string name { get; set; }
		public string avatar { get; set; }
		public List<Post> PostList;
		public List<Todo> TodoList;
		public Address address { get; set; }
	}
}
