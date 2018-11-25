using Mints.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamCardPin.Models.ApiModels
{
    public class FarmerProfileRequestModel : BaseModel
    {
       
        public FarmerViewModel Profile { get; set; }

    }

    public class FarmerViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime DateCreated { get; set; }

        public int TotalAnimals { get; set; }
    }

    public class RegisterFarmerRequestModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateRegistered { get; set; }
    }

    public class RegisterAnimalTrackerViewModel
    {
        [Required]
        public string AnimalId { get; set; }

        [Required]
        public string TrackerId { get; set; }
    }
}
