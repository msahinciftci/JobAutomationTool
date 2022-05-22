using System;

namespace TransferAutomation.AutomationConsole.Models
{
    public class SettingModel
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateFinish { get; set; }
        public int GroupStart { get; set; }
        public int GroupEnd { get; set; }
        public int? RowNumberStart { get; set; }
        public int? RowNumberFinish { get; set; }
        public int? IncreaseAmount { get; set; }
        public string IncreaseType { get; set; }
        public string Action { get; set; }
        public string ApplicationPath { get; set; }
    }
}
