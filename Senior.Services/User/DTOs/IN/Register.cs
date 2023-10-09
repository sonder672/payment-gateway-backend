using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Senior.Services.Helper;

namespace Senior.Services.User.DTOs.IN
{
    public class Register
    {
        public Register(string name, string email, string password)
        {
            Id = UUID.Generate();
            Name = name;
            Email = email;
            Password = password;
        }

        [JsonIgnore]
        public string Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Password { get; set; }
    }
}