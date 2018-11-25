using Mints.DLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Models.Farmer
{
    public class Farmer : IEntity
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class FarmerProfile : IEntity
    {
        public int Id { get; set; }

        public Farmer Farmer { get; set; }

        public int TotalAnimals { get; set; }

    }
}
