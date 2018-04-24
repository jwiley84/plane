using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace plane {

	public class Fan {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
        public int fanListID { get; set; }
        public int postID { get; set; }
        public Post post { get; set; }
        public int userID { get; set; }
        public User user { get; set; }
	}
}