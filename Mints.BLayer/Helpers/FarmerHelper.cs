using Mints.BLayer.Models.Farmer;
using System;
using System.Collections.Generic;
using System.Text;
using BFarmer = Mints.BLayer.Models.Farmer.Farmer;
using DFarmer = Mints.DLayer.Models.Farmer;

namespace Mints.BLayer.Helpers
{
    public static class FarmerHelper
    {
        public static BFarmer ToFarmer(this DFarmer dataModel) => new BFarmer
        {
            Id = dataModel.Id,
            Email = dataModel.Email,
            FirstName = dataModel.FirstName,
            Phone = dataModel.Phone,
            LastName = dataModel.LastName,
            Address = dataModel.Address,
            DateCreated = dataModel.DateCreated
        };



        public static DFarmer ToDbFarmer(this BFarmer farmer) => new DFarmer
        {
            Id = farmer.Id,
            Email = farmer.Email,
            Address = farmer.Address,
            Phone = farmer.Phone,
            FirstName = farmer.FirstName,
            LastName = farmer.LastName,
        };
    }
}
