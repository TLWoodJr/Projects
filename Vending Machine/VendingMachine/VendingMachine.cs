using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CapStone;
using VendingMachine.Exceptions;

namespace CapStone
{
    //vendingmachine.csv
    public class VendingMachine
    {
        public List<MachineItem> _purchaseList = new List<MachineItem>();
        public Dictionary<string, MachineItem> Inventory = new Dictionary<string, MachineItem>();
        public decimal CurrentBalance { get; private set; } = 0;
        public Report Report { get; }
        /// <summary>
        /// 
        /// </summary>
        public VendingMachine()
        {

            string currentDirectory = Environment.CurrentDirectory;
            string navigateToEtc = Path.Combine(currentDirectory, @"..\..\..\etc\");
            string fullPath = Path.Combine(navigateToEtc, "vendingmachine.csv");
            

            using (StreamReader sr = new StreamReader(fullPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lineArray = line.Split('|');
                    if (lineArray[3] == "Chip")
                    {
                        Inventory[lineArray[0]] = new Chip(lineArray[1], decimal.Parse(lineArray[2]));
                    }
                    if (lineArray[3] == "Candy")
                    {
                        Inventory[lineArray[0]] = new Candy(lineArray[1], decimal.Parse(lineArray[2]));
                    }
                    if (lineArray[3] == "Drink")
                    {
                        Inventory[lineArray[0]] = new Drink(lineArray[1], decimal.Parse(lineArray[2]));
                    }
                    if (lineArray[3] == "Gum")
                    {
                        Inventory[lineArray[0]] = new Gum(lineArray[1], decimal.Parse(lineArray[2]));
                    }
                }
            }

            Report = new Report(Inventory);

        }
        
        public void FeedMoney(decimal money)
        {
            if (money == 1M || money == 2M || money == 5M || money == 10M)
            {
                Log.WriteFeedMoney(this, money);
                CurrentBalance += money;
            }
            else
            {
                throw new BadBillException();
            }
                
        }
        /// <summary>
        /// Sells item assigned to <paramref name="key"/> and subtracts it from CurrentBalance.
        /// </summary>
        /// <param name="key">Key in Inventory represented by slot code</param>
        public void ProductSelect(string key)
        {
            Log.WriteProductSelect(this, key);
            MachineItem mi = Inventory[key];
            if (!mi.SoldOut)
            {
                if (mi.Price <= CurrentBalance)
                {
                    mi.Qty -= 1;
                    CurrentBalance -= mi.Price;
                    _purchaseList.Add(mi);
                    Report.ReportDict[mi.Name]++;
                    Report.AddToTotal(mi.Price);
                }
                else
                {
                    throw new InsufficientFundsException();
                }
            }
            else
            {
                throw new SoldOutException();
            }
        }



        public Change ReturnChange()
        {
            Log.WriteGiveChange(this);
            Change change = new Change(CurrentBalance);
            CurrentBalance = 0;
            return change;
        }
    }
}
