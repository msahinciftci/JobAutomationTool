using System;
using System.Configuration;
using TransferAutomation.AutomationConsole.Managers;

namespace TransferAutomation.AutomationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            JobAutomateManager automateManager = new JobAutomateManager();
            var Action = ConfigurationManager.AppSettings["Action"];
            if (Action == "PrintListByRowNumber")
                automateManager.AutomateByRowNumber();
            else if (Action == "PrintListByDate")
                automateManager.AutomateByDate();
            Console.ReadKey();
        }
    }
}
