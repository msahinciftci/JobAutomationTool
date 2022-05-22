using JobConsole.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace JobConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //args = new string[] { "PrintListByDate", "1", "2010-01-01T00:00:00", "2010-02-01T00:00:00" };
                //args = new string[] { "PrintListByRowNumber", "1", "1", "101" };
                if (args == null || args.Length < 4)
                    throw new ArgumentNullException("One or more arguments are missing!");
                var action = args[0];
                var group = int.Parse(args[1]);
                List<CustomerModel> items;
                using (StreamReader r = new StreamReader(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"Data\DataExample.json")))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<CustomerModel>>(json);
                }

                switch (action)
                {
                    case "PrintListByRowNumber":
                        var minIndex = int.Parse(args[2]);
                        var maxIndex = int.Parse(args[3]);
                        var customerList1 = items.Where(x => x.Group == group && x.RowNumber >= minIndex && x.RowNumber < maxIndex).ToList();
                        foreach (var customer in customerList1)
                        {
                            PrintCustomer(customer);
                        }
                        break;
                    case "PrintListByDate":
                        var minDate = DateTime.Parse(args[2]);
                        var maxDate = DateTime.Parse(args[3]);
                        var customerList2 = items.Where(x => x.Group == group && x.CreatedDate >= minDate && x.CreatedDate < maxDate).ToList();
                        foreach (var customer in customerList2)
                        {
                            PrintCustomer(customer);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private static void PrintCustomer(CustomerModel customer)
        {
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine($"Row Number: {customer.RowNumber}");
            Console.WriteLine($"Group: {customer.Group}");
            Console.WriteLine($"First Name: {customer.FirstName}");
            Console.WriteLine($"Last Name: {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Gender: {customer.Gender}");
            Console.WriteLine($"Ip Address: {customer.IpAddress}");
            Console.WriteLine($"Created Date: {customer.CreatedDate:dd/MM/yyyy HH:mm:ss}");
            Thread.Sleep(150);
        }
    }
}
