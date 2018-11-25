using Mints.BLayer.Models.Identity;
using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BRole = Mints.BLayer.Models.Identity.Role;
using DRole = Mints.DLayer.Models.Role;

namespace Mints.BLayer.Helpers
{
    static class RoleHelper
    {
        public static BRole ToRole(this DRole dataModel) => new BRole
        {
            Id = dataModel.Id,
            Name = dataModel.Name,            
        };



        public static DRole ToDbRole(this BRole role) => new DRole
        {
            Id = role.Id,
           Name = role.Name
        };
    }
}
