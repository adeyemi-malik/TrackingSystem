
using Mints.DLayer.Models.Base;
using System;

namespace Mints.BLayer.Models.Identity
{
    public partial class User : IEntity
    {
        public User()
        {

        }
        public User(string username)
        {
            UserName = username;
        }

        public User(int id, string username)
        {
            Id = id;
            UserName = username;
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
      
        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public string HashSalt { get; set; }

        public DateTime DateCreated { get; set; }

    }


    public class Role : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}