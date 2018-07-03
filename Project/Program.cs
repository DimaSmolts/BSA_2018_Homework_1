﻿using System;
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
						break;
					case "2":
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
	}
}