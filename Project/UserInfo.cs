using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class UserInfo
	{
		public User user { get; set; }
		public Post lastPost { get; set; }
		public Post mostCommentsPost { get; set; }
		public Post mostLikesPost { get; set; }
		public int lastPostComments { get; set; }
		public int notCompleteddTodo { get; set; }
	}
}
