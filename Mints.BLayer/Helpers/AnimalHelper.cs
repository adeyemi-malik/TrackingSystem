using System;
using System.Collections.Generic;
using System.Text;
using BAnimal = Mints.BLayer.Models.Animal.Animal;
using DAnimal = Mints.DLayer.Models.Animal;

namespace Mints.BLayer.Helpers
{
    public static class AnimalHelper
    {
        public static BAnimal ToAnimal(this DAnimal dataModel) => new BAnimal
        {
            Id = dataModel.Id,
            Tag = dataModel.Tag,
            Status = dataModel.Status,           
            Picture = dataModel.Picture,
            DateCreated = dataModel.DateCreated
        };



        public static DAnimal ToDbAnimal(this BAnimal animal) => new DAnimal
        {
            Id = animal.Id,
            Tag = animal.Tag,
            Status = animal.Status,
            Picture = animal.Picture,
        };
    }
}
