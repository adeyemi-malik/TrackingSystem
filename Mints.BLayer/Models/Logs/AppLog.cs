using Mints.DLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Models.Logs
{
    public class AppLog : IEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }

        public string LogEntry { get; set; }
    }
}
