using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace DatingApp.API.models
{
    public class User
    {
        
        public int id { get; set; }
        public string Username { get; set; }
        public int rol { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LatActive { get; set; }
        public string Introduction { get; set; }
        public string lookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
        [JsonProperty("Id")]
        public int Id { get; internal set; }

        public User()
        {
            Photos = new Collection<Photo>();
        }


    }


}