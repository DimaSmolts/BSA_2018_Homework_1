﻿using System;
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
		public List<Post> PostList = new List<Post>();
		public List<Todo> TodoList = new List<Todo>();
		public Address address { get; set; }
	}
}
