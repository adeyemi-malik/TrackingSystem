using Mints.DLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Models.Tracker
{
    public class Tracker: IEntity
    {
        public int Id { get; set; }

        public string Tag { get; set; }

        public int Status { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
