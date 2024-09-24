using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.Controllers
{
    public class Artist
    {

        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public byte[]? Salt { get; set; }



    }
}