using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mints.Models.ApiModels
{
    public class TrackerViewModel
    {
    }

    public class RegisterTrackerViewModel
    {
        [Required]
        public string Tag { get; set; }

        public bool Status { get; set; } = true;
    }

    public class RegisterTrackerRequestModel : BaseModel
    {
        public string Tag { get; set; }

        public DateTime DateRegistered { get; set; }
    }
}
