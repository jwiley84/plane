using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace plane {

	public class Post {
	
		public int postID { get; set; }
		
		public string postContent { get; set; }
	
		public int userID { get; set; }
	
		public User author { get; set; }
	
		public DateTime created_at { get; set; }
	
		public DateTime updated_at { get; set; }
		public int deleted { get; set; }
	
		public List<Fan> fans { get; set; }
	
		public Post()
		{
			fans = new List<Fan>();
		}
	}
}