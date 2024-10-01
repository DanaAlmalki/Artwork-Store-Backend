using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.DTO
{
    public class ArtistDTO
    {
        // create artist
        public class ArtistCreateDto
        {
            public string Name { set; get; }
            public string Description { set; get; }
            public string Email { set; get; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
            public byte[]? Salt { get; set; }


        }

        // read data = get data
        public class ArtistReadDto
        {
            public Guid Id { get; set; }
            public string Name { set; get; }
            public string Description { set; get; }
            public string Email { set; get; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }

        }

        // update
        public class ArtistUpdateDto
        {
            public string Name { set; get; }
            public string Description { set; get; }
            public string Email { set; get; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }

        }
    }
}