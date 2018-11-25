using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mints.Models.ApiModels
{
    public class AnimalViewModel
    {
        public int Id { get; set; }

        public string Tag { get; set; }

        public bool Status { get; set; }

        public byte[] Picture { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class RegisterAnimalViewModel
    {
        [Required]
        public string Tag { get; set; }

        public bool Status { get; set; } = true;
    }

    public class RegisterAnimalRequestModel : BaseModel
    {
        public string Tag { get; set; }

        public DateTime DateRegistered { get; set; }
    }

    public class AnimalsRequestModel : BaseModel
    {
        public AnimalsRequestModel()
        {
            Animals = new List<AnimalViewModel>();
        }

        public IEnumerable<AnimalViewModel> Animals { get; set; }
    }

    public class AnimalRequestModel : BaseModel
    {     
        public AnimalViewModel Animal { get; set; }
    }

    public class AnimalCountRequestModel : BaseModel
    {
        public int Count { get; set; }
    }
}
