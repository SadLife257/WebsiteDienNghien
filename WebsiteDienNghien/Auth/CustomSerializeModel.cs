using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDienNghien.Auth
{
    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
    }
}