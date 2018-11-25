using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class ApiClient: BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 250)]
        public string ClientName { get; set; }

        [Required]
        [StringLength(maximumLength: 750)]
        public string CallBackUrl { get; set; }

        [Required]
        [StringLength(maximumLength: 500)]
        public string ApiKeyHash { get; set; }
       
    }
}
