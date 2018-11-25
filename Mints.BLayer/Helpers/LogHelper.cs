using System;
using System.Collections.Generic;
using System.Text;
using BAppLog = Mints.BLayer.Models.Logs.AppLog;
using DAppLog = Mints.DLayer.Models.AppLog;

namespace Mints.BLayer.Helpers
{
    public static class LogHelper
    {
        public static BAppLog ToAppLog(this DAppLog dataModel) => new BAppLog
        {
            Id = dataModel.Id,
            Key = dataModel.Key,
            LogEntry = dataModel.LogEntry
        };



        public static DAppLog ToDbAppLog(this BAppLog appLog) => new DAppLog
        {
            Id = appLog.Id,
            Key = appLog.Key,
            LogEntry = appLog.LogEntry
        };
    }
}
