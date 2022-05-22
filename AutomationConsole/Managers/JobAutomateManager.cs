using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using TransferAutomation.AutomationConsole.Models;

namespace TransferAutomation.AutomationConsole.Managers
{
    public class JobAutomateManager
    {
        public void AutomateByDate()
        {
            var setting = GetSettings(true);
            DateTime automationStartDate = DateTime.Now;
            Console.WriteLine("End Date: " + setting.DateFinish.Value.ToString("dd/MM/yyyy HH:mm:ss"));

            for (int currentGroup = setting.GroupStart; currentGroup < setting.GroupEnd; currentGroup++)
            {
                DateTime? currentMin = setting.DateStart;
                DateTime? currentMax = GetIncreasedDate(setting.DateStart, setting.IncreaseAmount.Value, setting.IncreaseType);
                do
                {
                    var localStart = DateTime.Now;
                    if (!ProgramIsRunning(setting.ApplicationPath))
                    {
                        Console.WriteLine("**************************************************************");
                        Console.WriteLine("Current Group Start Date: " + localStart.ToString("dd/MM/yyyy HH:mm:ss"));
                        Console.WriteLine("Current Group: " + currentGroup);
                        Console.WriteLine("Current Date Start: " + currentMin?.ToString("dd/MM/yyyy HH:mm:ss"));
                        Console.WriteLine("Current Date End: " + currentMax?.ToString("dd/MM/yyyy HH:mm:ss"));

                        Process process = new Process();
                        process.StartInfo.FileName = setting.ApplicationPath;
                        process.StartInfo.Arguments = setting.Action + " " + currentGroup + " " + currentMin.Value.ToString("yyyy-MM-ddTHH:mm:ss") + " " + currentMax.Value.ToString("yyyy-MM-ddTHH:mm:ss");
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        process.StartInfo.CreateNoWindow = false;
                        process.Start();
                        process.WaitForExit();

                        currentMin = currentMax;
                        currentMax = GetIncreasedDate(currentMin, setting.IncreaseAmount.Value, setting.IncreaseType);
                    }
                    var datetimediff = new DateTime((DateTime.Now - localStart).Ticks);
                    var elapsedTime = datetimediff.ToString("HH:mm:ss");
                    Console.WriteLine("Elapsed time for current job: " + elapsedTime);
                } while (currentMin <= setting.DateFinish);
            }
            var diff = new DateTime((DateTime.Now - automationStartDate).Ticks);
            Console.WriteLine("Total elapsed time: " + diff.ToString("HH:mm:ss"));
        }

        public void AutomateByRowNumber()
        {
            var setting = GetSettings(false);
            DateTime automationStartDate = DateTime.Now;

            for (int currentGroup = setting.GroupStart; currentGroup < setting.GroupEnd; currentGroup++)
            {
                int? currentMin = setting.RowNumberStart;
                int? currentMax = setting.RowNumberStart + setting.IncreaseAmount;
                do
                {
                    var localStart = DateTime.Now;
                    if (!ProgramIsRunning(setting.ApplicationPath))
                    {
                        Console.WriteLine("**************************************************************");
                        Console.WriteLine("Current Job Start Date: " + localStart.ToString("dd/MM/yyyy HH:mm:ss"));
                        Console.WriteLine("Current Group: " + currentGroup);
                        Console.WriteLine("Current Number Start: " + currentMin);
                        Console.WriteLine("Current Number End: " + currentMax);

                        Process process = new Process();
                        // Configure the process using the StartInfo properties.
                        process.StartInfo.FileName = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + setting.ApplicationPath);
                        process.StartInfo.Arguments = $@"{setting.Action} {currentGroup} {currentMin} {currentMax}";
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        process.StartInfo.CreateNoWindow = false;
                        process.Start();
                        process.WaitForExit();// Waits here for the process to exit.

                        currentMin = currentMax;
                        currentMax = currentMin + setting.IncreaseAmount;
                    }
                    var datetimediff = new DateTime((DateTime.Now - localStart).Ticks);
                    var elapsedTime = datetimediff.ToString("HH:mm:ss");
                    Console.WriteLine("Elapsed time for current job: " + elapsedTime);
                } while (currentMin <= setting.RowNumberFinish);
            }
            var diff = new DateTime((DateTime.Now - automationStartDate).Ticks);
            Console.WriteLine("Total elapsed time: " + diff.ToString("HH:mm:ss"));
        }

        private DateTime? GetIncreasedDate(DateTime? startDate, int increaseAmount, string increaseType)
        {
            var endDate = startDate;
            switch (increaseType)
            {
                case "Minute":
                    endDate = startDate.Value.AddMinutes(increaseAmount);
                    break;
                case "Hour":
                    endDate = startDate.Value.AddHours(increaseAmount);
                    break;
                case "Day":
                    endDate = startDate.Value.AddDays(increaseAmount);
                    break;
                case "Month":
                    endDate = startDate.Value.AddMonths(increaseAmount);
                    break;
                case "Year":
                    endDate = startDate.Value.AddYears(increaseAmount);
                    break;
            }
            return endDate;
        }

        private SettingModel GetSettings(bool isDateSetting)
        {
            var setting = new SettingModel();
            setting.GroupStart = ConfigurationManager.AppSettings["GroupStart"] != null ? int.Parse(ConfigurationManager.AppSettings["GroupStart"]) : 0;
            setting.GroupEnd = ConfigurationManager.AppSettings["GroupEnd"] != null ? int.Parse(ConfigurationManager.AppSettings["GroupEnd"]) : 0;
            setting.Action = ConfigurationManager.AppSettings["Action"] ?? "";
            setting.ApplicationPath = ConfigurationManager.AppSettings["ApplicationPath"] ?? @"\Application\JobConsole.exe";
            setting.IncreaseAmount = ConfigurationManager.AppSettings["IncreaseAmount"] != null ? int.Parse(ConfigurationManager.AppSettings["IncreaseAmount"]) : 1;
            if (isDateSetting)
            {
                setting.DateStart = ConfigurationManager.AppSettings["DateStart"] != null ? DateTime.Parse(ConfigurationManager.AppSettings["DateStart"]) : DateTime.Now.AddMonths(-1);
                setting.DateFinish = ConfigurationManager.AppSettings["DateFinish"] != null ? DateTime.Parse(ConfigurationManager.AppSettings["DateFinish"]) : DateTime.Now;
                setting.IncreaseType = ConfigurationManager.AppSettings["IncreaseType"] ?? "Day";
            }
            else
            {
                setting.RowNumberStart = ConfigurationManager.AppSettings["RowNumberStart"] != null ? int.Parse(ConfigurationManager.AppSettings["RowNumberStart"]) : 0;
                setting.RowNumberFinish = ConfigurationManager.AppSettings["RowNumberFinish"] != null ? int.Parse(ConfigurationManager.AppSettings["RowNumberFinish"]) : 0;
            }
            return setting;
        }

        private bool ProgramIsRunning(string FullPath)
        {
            string FilePath = Path.GetDirectoryName(FullPath);
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();
            bool isRunning = false;

            Process[] pList = Process.GetProcessesByName(FileName);

            foreach (Process p in pList)
            {
                if (p.MainModule.FileName.StartsWith(FilePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    isRunning = true;
                    break;
                }
            }

            return isRunning;
        }
    }
}
