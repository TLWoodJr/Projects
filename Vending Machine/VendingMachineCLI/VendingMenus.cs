using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Exceptions;



namespace CapStone
{

    public class VendingMenus
    {
        private VendingMachine VendingMachine { get; }

        public VendingMenus(VendingMachine vm)
        {
            VendingMachine = vm;
        }

        /// <summary>
        /// This starts the vending machine
        /// </summary>
        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1)Display Items");
                Console.WriteLine("2)Purchase an Item");
                Console.WriteLine("q)Quit");

                char key = Console.ReadKey().KeyChar;
                if (key == 'q')
                {
                    VendingMachine.Report.CombineReport();
                    exit = true;

                }
                else if (key == '1')
                {
                    DisplayMenu();
                }
                else if (key == '2')
                {
                    PurchaseMenu();
                }
            }
        }

        private void DisplayMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                foreach (var item in VendingMachine.Inventory)
                {
                    Console.WriteLine($"{item.Key.PadRight(3, ' ')} " +
                                      $"{item.Value.Name.PadRight(19, ' ')} " +
                                      $"{item.Value.Price.ToString("C").PadRight(10, ' ')}" +
                                      $"{item.Value.DisplayQty}");
                }
                Console.WriteLine();
                Console.WriteLine("q)Quit");
                char key = Console.ReadKey().KeyChar;
                if (key == 'q')
                {
                    exit = true;
                }
            }
        }
        private void PurchaseMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1)Feed Money");
                Console.WriteLine("2)Select Product");
                Console.WriteLine("3)Finish Transaction");
                Console.WriteLine($"Current Money Provided: {VendingMachine.CurrentBalance.ToString("C")}");

                char key = Console.ReadKey().KeyChar;
                
                if (key == '1')
                {
                    try
                    {
                        FeedMoney();
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Please enter valid dollar amount (1,2,5,10).");
                        Console.ReadKey();
                    }
                }
                else if (key == '2')
                {
                    //If sale is successful
                    //1. Selected product exists
                    //2. There is enough money in CurrentBalance for sale
                    //3. Product is not sold out
                    try
                    {
                        SelectProduct();
                    }
                    //1. Write this message if user types invalid slot
                    catch (KeyNotFoundException)
                    {
                        Console.WriteLine("Product code does not exist.");
                        Console.ReadKey();
                    }
                    //2. Write this message if there is not enough money in vm.CurrentBalance
                    catch (InsufficientFundsException)
                    
                    {
                        Console.WriteLine("Not enough money. Press any key to continue, then feed machine more money to buy that item.");
                        Console.ReadKey();
                    }
                    //3. Write this message if the item is sold out
                    catch (SoldOutException)
                    {
                        Console.WriteLine("That item is sold out.");
                        Console.ReadKey();
                    }
                }
                else if (key == '3')
                {
                    Console.WriteLine();
                    FinishTransaction();
                    Console.ReadKey();
                    exit = true;

                }
            }
        }

        /// <summary>
        /// Takes money from user. Requires that they enter 1,2, 5, or 10
        /// </summary>
        private void FeedMoney()
        {
            Console.Clear();
            Console.WriteLine("Enter amount to feed: $");
            decimal money = decimal.Parse(Console.ReadLine());
            try
            {
                VendingMachine.FeedMoney(money);
            }
            catch(BadBillException)
            {
                Console.WriteLine("Invalid Bill");
                Console.ReadKey();
            }
            
        }

        private void SelectProduct()
        {
            Console.Clear();
            foreach (var item in VendingMachine.Inventory)
            {
                Console.WriteLine($"{item.Key.PadRight(3, ' ')} " +
                                  $"{item.Value.Name.PadRight(23, ' ')} " +
                                  $"{item.Value.Price.ToString("C").PadRight(10, ' ')}" +
                                  $"{item.Value.DisplayQty}");
            }
            Console.WriteLine();
            Console.Write("Enter Product Code: ");
            string userProductCode = Console.ReadLine().ToUpper();
            
            VendingMachine.ProductSelect(userProductCode);
            // Prints out the item that was selected
            Console.WriteLine($"Dispensing: {VendingMachine.Inventory[userProductCode].Name}");
            Console.ReadKey();    
        }

        private void FinishTransaction()
        {
            Change chg = VendingMachine.ReturnChange();
            
            Console.Clear();
            Console.WriteLine("Thanks for your business!");
            Console.WriteLine($"Your change: {chg.Quarters} quarters, {chg.Dimes} dimes, and {chg.Nickels} nickels.");
            if (VendingMachine._purchaseList.Count > 0)
            {
                foreach (var item in VendingMachine._purchaseList)
                {
                    Console.WriteLine(item.Sound);
                }
            }
            else
            {
                Console.WriteLine("Remember: this is not a change machine!");
            }
            VendingMachine._purchaseList.Clear();
        }
    }
}
