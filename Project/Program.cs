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
				Console.WriteLine("  1 ");
				Console.WriteLine("  2 ");
				Console.WriteLine("  3 ");
				Console.WriteLine("  4 ");
				Console.WriteLine("  5 ");
				Console.WriteLine("  6 ");
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
						break;
					case "4":
						break;
					case "5":
						break;
					case "6":
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