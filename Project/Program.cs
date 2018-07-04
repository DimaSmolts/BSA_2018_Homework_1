using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Project
{
	class Program
	{
		public static string URL = "https://5b128555d50a5c0014ef1204.mockapi.io";
		public static List<User> myUserDB;

		static void Main(string[] args)
		{
			myUserDB = SendRequest();
			Console.WriteLine("Hello Binary Studio Academy 2018!");
			while (true)
			{
				Console.WriteLine("\n*******************************************");
				Console.WriteLine("  choose the task (enter only one number)");
				Console.WriteLine("  1 - Get amount of comments (by user.id)");
				Console.WriteLine("  2 - Get comment list (by user.id)");
				Console.WriteLine("  3 - Get completed todos (by user.id)");
				Console.WriteLine("  4 - Get list of ordered users and todos");
				Console.WriteLine("  5 - Get user info (by user.id)");
				Console.WriteLine("  6 - Get post info (by post.id)");
				Console.WriteLine("  7 - Update");
				Console.WriteLine("  8 - Exit");
				Console.WriteLine("*******************************************");
				Console.Write("===> ");
				string input = Console.ReadLine();
				Console.WriteLine("*******************************************");
				switch (input)
				{
					case "1":
						CommentAmount();						
						break;
					case "2":
						Comments();
						break;
					case "3":
						CompletedTodo();
						break;
					case "4":
						AllUsersTodo();
						break;
					case "5":
						UserInfo();
						break;
					case "6":
						PostInfo();
						break;
					case "7":
						myUserDB = SendRequest();
						Console.WriteLine("Data was updated!");
						break;
					case "8":
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("wrong input");
						break;
				}
			}
		}
		public static void CommentAmount()
		{
			int? tempIndex = IdInput();

			if (tempIndex != null && (tempIndex > 0 && tempIndex < myUserDB.Count)) 
			{
				var respo = from p in								
								(from u in myUserDB
								 where u.id == tempIndex 
								 select u.PostList).Single()								
							select new { Title = p.title, Amount = p.CommentList.Count };
				
				if (respo.ToList().Count != 0)
					foreach (var item in respo)
						Console.WriteLine($"Title:{item.Title}, Comment amount: {item.Amount}");
				else
					Console.WriteLine("no posts");				
			}
			else
			{
				Console.WriteLine("wrong id");
			}
		}
		public static void Comments()
		{
			int? tempIndex = IdInput();

			if (tempIndex != null && (tempIndex > 0 && tempIndex < myUserDB.Count))
			{
				var selectedPostList = (from u in myUserDB
									   where u.id == tempIndex 
									   select u.PostList).First();
				if (selectedPostList.Count() != 0)
				{
					var selectedCommentLists = (from p in selectedPostList
												select p.CommentList).First();

					if (selectedCommentLists.Count() != 0)
					{
						var selectedComments = from c in selectedCommentLists
											   where c.body.Length < 50
											   select c;
						if (selectedComments.Count() != 0)
						{
							foreach (Comment com in selectedComments)
								Console.WriteLine($"Comment: {com.body}");
						}
						else
						{
							Console.WriteLine("all coments are more than 50 characters");
						}
					}
					else
					{
						Console.WriteLine("no comments");
					}
				}
				else
				{
					Console.WriteLine("no posts");
				}
			}
		}
		public static void CompletedTodo()
		{
			int? tempIndex = IdInput();

			if (tempIndex != null && (tempIndex > 0 && tempIndex < myUserDB.Count))
			{
				var todos = (from u in myUserDB
							where u.id == tempIndex
							select u.TodoList).First();
				if (todos.Count != 0)
				{
					var comletedtodo = from t in todos
									   where t.isComplete == true
									   select t;
					if (comletedtodo.Count() != 0)
						foreach (var item in comletedtodo)
							Console.WriteLine($"Is Completed: {item.id} - {item.name}");
					else
						Console.WriteLine("no completed todos");
				}
				else
				{
					Console.WriteLine("no todos");
				}
			}
			else
			{
				Console.WriteLine("wrong id");
			}
		}
		public static void AllUsersTodo()
		{
				foreach(User u in myUserDB.OrderBy( user => user.name))
				{
					Console.WriteLine($"{u.id} {u.name}");
				if (u.TodoList.Count() != 0)
					foreach (Todo t in u.TodoList.OrderByDescending(todo => todo.name.Length))
						Console.WriteLine($"\t{(t.isComplete ? '+' : '-')}\ttodo:{t.name}");
				else
					Console.WriteLine("\t\tno todos");					
				}
		}
		public static void UserInfo()
		{
			int? tempIndex = IdInput();

			if (tempIndex != null && (tempIndex > 0 && tempIndex < myUserDB.Count))
			{
				UserInfo UI = new UserInfo();

				var selectedUser = (from u in myUserDB
									where u.id == tempIndex
									select u).First();
				UI.user = selectedUser;
				if (selectedUser.PostList.Count != 0)
				{
					var lastPost = (from p in selectedUser.PostList.OrderByDescending(d => d.createdAt)
									select p).First();
					UI.lastPost = lastPost;

					var lastPostComments = lastPost.CommentList.Count;
					UI.lastPostComments = lastPostComments;

					var mostCommentsPost = (from p in selectedUser.PostList
											select p)
											.Aggregate((i1, i2) => i1.CommentList.Count(x => x.body.Length>80) > i2.CommentList.Count(x => x.body.Length > 80) ? i1 : i2);
					UI.mostCommentsPost = mostCommentsPost;

					var mostLikesPost = (from p in selectedUser.PostList
										 select p)
									 .Aggregate((i1, i2) => i1.likes > i2.likes ? i1 : i2);
					UI.mostLikesPost = mostLikesPost;

				}

				var todos = (from u in myUserDB
							 where u.id == tempIndex							
							 select u.TodoList).First();
				var notComletedtodo = (from t in todos
									   where t.isComplete == false
									   select t).Count();
				UI.notCompleteddTodo = notComletedtodo;


				Console.WriteLine($"User: {UI.user.id} {UI.user.name}\n"+
								  $"Last Post: {UI.lastPost}\n" +
								  $"Comments: {UI.lastPostComments}\n" +
								  $"Most commented: {UI.mostCommentsPost}\n" +
								  $"Most likes: {UI.mostLikesPost}\n" +
								  $"Not completed: {UI.notCompleteddTodo}\n");				


			}
			else
			{
				Console.WriteLine("wrong id");
			}
		}
		public static void PostInfo()
		{
			int? tempIndex;

			Console.WriteLine("\nWrite the id of post");
			Console.Write("Post id # ");
			try
			{
				tempIndex = Convert.ToInt32(Console.ReadLine());

			}
			catch (FormatException)
			{
				Console.WriteLine("wrong data");
				tempIndex = null;
			}

			if (tempIndex != null && tempIndex > 0)
			{
				PostInfo PI = new PostInfo();


				var BigList = from u in myUserDB
						   where u.PostList.Count != 0
						   select u.PostList;
				
				List<Post> temp = new List<Post>();
				foreach (List<Post> lp in BigList.ToList())
					temp.AddRange(lp);

				var selectedPost = (from p in temp
									where p.id == tempIndex
									select p).First();
				PI.post = selectedPost;

				if(selectedPost.CommentList.Count != 0)
				{
					var longComment = (from c in selectedPost.CommentList
									  select c)
									  .Aggregate((i1, i2) => i1.body.Length > i2.body.Length ? i1 : i2);
					PI.longComment = longComment;

					var likedComment = (from c in selectedPost.CommentList
										select c)
										.Aggregate((i1, i2) => i1.likes > i2.likes ? i1 : i2);
					PI.likedComment = likedComment;

					var specialComment = (from c in selectedPost.CommentList
										  where c.likes == 0 || c.body.Length < 80
										  select c).Count();
					PI.SpecialComment = specialComment;
				}
				Console.WriteLine($"Post: {PI.post.id} {PI.post.title}\n" +
								  $"LongComment: {PI.longComment}\n" +
								  $"LikedComment: {PI.likedComment}\n" +
								  $"Amount special: {PI.SpecialComment}\n");		  
			}
			else
			{
				Console.WriteLine("wrong id");
			}

		}
		public static List<User> SendRequest()
		{
			var client = new HttpClient();

			var userResponse = client.GetAsync(URL + "/users").Result;
			var userJSON = userResponse.Content.ReadAsStringAsync().Result;
			List<User> userList = JsonConvert.DeserializeObject<List<User>>(userJSON);

			var postResponse = client.GetAsync(URL + "/posts").Result;
			var postJSON = postResponse.Content.ReadAsStringAsync().Result;
			List<Post> postList = JsonConvert.DeserializeObject<List<Post>>(postJSON);

			var commentResponse = client.GetAsync(URL + "/comments").Result;
			var commentJSON = commentResponse.Content.ReadAsStringAsync().Result;
			List<Comment> commentList = JsonConvert.DeserializeObject<List<Comment>>(commentJSON);

			var todoResponse = client.GetAsync(URL + "/todos").Result;
			var todoJSON = todoResponse.Content.ReadAsStringAsync().Result;
			List<Todo> todoList = JsonConvert.DeserializeObject<List<Todo>>(todoJSON);

			var addressResponse = client.GetAsync(URL + "/address").Result;
			var addressJSON = addressResponse.Content.ReadAsStringAsync().Result;
			List<Address> addressList = JsonConvert.DeserializeObject<List<Address>>(addressJSON);

			foreach (Post p in postList)
			{
				var selectedComments = from c in commentList
									   where p.id == c.PostId
									   select c;
				p.CommentList = selectedComments.ToList();
			}

			foreach (User u in userList)
			{
				var selectedPost = from p in postList
								   where u.id == p.userId
								   select p;
				if (selectedPost.ToList().Count != 0)
					u.PostList = selectedPost.ToList();


				var selectedTodo = from t in todoList
								   where u.id == t.userId
								   select t;
				if (selectedTodo.ToList().Count != 0)
					u.TodoList = selectedTodo.ToList();


				var selectedAddress = from a in addressList
									  where u.id == a.userId
									  select a;
				if (selectedAddress.ToList().Count != 0)
					u.address = selectedAddress.ToList()[0];
			}
			return userList;
		}

		public static int? IdInput()
		{
			int? tempIndex;

			Console.WriteLine("\nWrite the id of user");
			Console.Write("User id # ");
			try
			{
				tempIndex = Convert.ToInt32(Console.ReadLine());

			}
			catch (FormatException)
			{
				Console.WriteLine("wrong data");
				tempIndex = null;
			}
			return tempIndex;
		}


	}
}