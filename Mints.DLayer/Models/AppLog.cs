using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class AppLog: BaseEntity
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string LogEntry { get; set; }
    }
}
