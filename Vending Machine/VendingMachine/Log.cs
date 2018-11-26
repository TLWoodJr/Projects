﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CapStone
{    
    public class Log
    {
        private static void WriteToLog(string message)
        {
            string destination = @"..\..\..\etc\log.txt";
            using (FileStream fs = File.Open(destination, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(message);
                }
            }
        }
        
        public static void WriteFeedMoney(VendingMachine vm, decimal money)
        {
            //DateTime.Now.ToString("yyyyMMddHHmmss"); // case sensitive
            WriteToLog($"{DateTime.Now.ToString().PadRight(19, ' ')} " +
                        "FEED MONEY".PadRight(23, ' ') +
                        $"{money.ToString("C").PadRight(7, ' ')} " +
                        $"{(vm.CurrentBalance + money).ToString("C")}");
        }

        public static void WriteProductSelect(VendingMachine vm, string userProductCode)
        {
            WriteToLog($"{DateTime.Now.ToString().PadRight(19, ' ')} " +
                       $"{vm.Inventory[userProductCode].Name} {userProductCode.ToUpper()} ".PadRight(23, ' ') +
                       $"{vm.CurrentBalance.ToString("C").PadRight(7, ' ')} " +
                       $"{(vm.CurrentBalance - vm.Inventory[userProductCode].Price).ToString("C")}");
        }

        public static void WriteGiveChange(VendingMachine vm)
        {
            WriteToLog($"{DateTime.Now.ToString().PadRight(19, ' ')} " +
                       "GIVE CHANGE".PadRight(23, ' ') +
                       $"{vm.CurrentBalance.ToString("C").PadRight(7, ' ')} " +
                       $"{0.ToString("C")}");
        }
    }
}
