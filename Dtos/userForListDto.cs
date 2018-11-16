using System;

namespace DatingApp.API.Dtos
{
    public class userForListDto
    {
          public int id { get; set; }
        public string Username { get; set; }
        public int rol { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }//edad en ves de la de nacimieento
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LatActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        public string Photourl { get; set; }
    }
}