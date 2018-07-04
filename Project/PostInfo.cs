using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class PostInfo
	{
		public Post post { get; set; }

		public Comment longComment { get; set; }
		public Comment likedComment { get; set; }
		public int SpecialComment { get; set; }
	}
}
