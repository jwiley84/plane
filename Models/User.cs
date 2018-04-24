using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace plane {

	public class User {

		 [Key]
		public int userID {get; set;}
        public string email { get; set; }
        public string alias { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public DateTime created_at { get; set; }
        
        public DateTime updated_at { get; set; }
        
        public List<Fan> fans { get; set; }
        public User()
        {
            fans = new List<Fan>();
        }

        
	}
}