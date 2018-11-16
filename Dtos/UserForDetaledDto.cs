using System;
using System.Collections.Generic;
using DatingApp.API.models;

namespace DatingApp.API.Dtos
{
    public class UserForDetaledDto
    {
        public int id { get; set; }
        public string Username { get; set; }
        public int rol { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LatActive { get; set; }
        public string Introduction { get; set; }
        public string lookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Photourl { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}