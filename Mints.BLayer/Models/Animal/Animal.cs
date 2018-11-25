using Mints.DLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Models.Animal
{
    public class Animal : IEntity
    {
        public int Id { get; set; }

        public string Tag { get; set; }

        public int Status { get; set; }

        public byte[] Picture { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
