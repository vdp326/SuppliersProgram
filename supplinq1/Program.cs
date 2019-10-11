using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supplinq1
{
    class Program
    {

        static void Main(string[] args)
        {
            // this will create array to store table data
            var suppliers = new[] {
                new {SN = 1, SName = "Smith", Status = 20, City = "London"},
                new {SN = 2, SName = "Jones", Status = 10, City = "Paris" },
                new {SN = 3, SName = "Blake", Status = 30, City = "Paris" },
                new {SN = 4, SName = "Clark", Status = 20, City = "London"},
                new {SN = 5, SName = "Adams", Status = 30, City = "Athens" },
            };

            var parts = new[] {
                new {PN = 1, PName = "Nut", Color = "Red", Weight = 12, City = "London"},
                new {PN = 2, PName = "Bolt", Color = "Green", Weight = 17, City = "Paris"},
                new {PN = 3, PName = "Screw", Color = "Blue", Weight = 17, City = "Rome"},
                new {PN = 4, PName = "Screw", Color = "Red", Weight = 14, City = "London"},
                new {PN = 5, PName = "Cam", Color = "Blue", Weight = 12, City = "Paris"},
                new {PN = 6, PName = "Cog", Color = "Red", Weight = 19, City = "London"}
            };

            var shipment = new[] {
                new {SN = 1, PN = 1, Qty = 300},
                new {SN = 1, PN = 2, Qty = 200},
                new {SN = 1, PN = 3, Qty = 400},
                new {SN = 1, PN = 4, Qty = 200},
                new {SN = 1, PN = 5, Qty = 100},
                new {SN = 1, PN = 6, Qty = 100},
                new {SN = 2, PN = 1, Qty = 300},
                new {SN = 2, PN = 2, Qty = 400},
                new {SN = 3, PN = 2, Qty = 200},
                new {SN = 4, PN = 2, Qty = 200},
                new {SN = 4, PN = 4, Qty = 300},
                new {SN = 4, PN = 5, Qty = 400}
            };

            // this will create menu and control variable
            string menuid = "1", colr = "";
            int suppno = 0;
            
            while(menuid!="0")
            {
                // this will display header in the project
                Console.WriteLine("Main Menu");
                Console.Clear();
                Console.WriteLine("************************************************************************************************************");
                Console.WriteLine("                                    Welcome to The ABC Manufacturing Company");
                Console.WriteLine("*************************************************************************************************************\n\n\n");

                Console.WriteLine("1. Display the content of each of the arrays: suppliers, parts, and shipment");
                Console.WriteLine("2. Display City name by part Color");
                Console.WriteLine("3. Query suppliers data and display only the suppliers names in ascending order");
                Console.WriteLine("4. Query and display the orders for a particular supplier");
                Console.WriteLine("0. For Exit  \n\n ");

                Console.Write("Enter your choice: ");

                menuid = Console.ReadLine();

                // it will select case based on menuid entered
                switch (menuid)
                {
                    case "1":
                        // it will create enumerable interface to access all three arrays
                        IEnumerable<string> suppliersDetails =
                            suppliers.Select(supp => $"{supp.SN} {supp.SName} {supp.Status} {supp.City}");
                        IEnumerable<string> partsDetails =
                            parts.Select(part => $"{part.PN} {part.PName} {part.Color} {part.Weight} {part.City}");
                        IEnumerable<string> shipmentDetails =
                                                shipment.Select(ship => $"{ship.SN} {ship.PN} {ship.Qty}");

                        Console.Clear();
                        // it will display results form enumerable
                        Console.WriteLine("Supplier Details");
                        foreach (string temp in suppliersDetails)
                        {
                            Console.WriteLine(temp);
                        }
                        Console.WriteLine("\nParts Details");
                        foreach (string temp in partsDetails)
                        {
                            Console.WriteLine(temp);
                        }
                        Console.WriteLine("\nShipment Details");
                        foreach (string temp in shipmentDetails)
                        {
                            Console.WriteLine(temp);
                        }
                        Console.ReadLine();
                        break;

                    case "2":
                        // it will read color name from user
                        Console.Write("Please enter Color name: ");
                        colr = Console.ReadLine();

                        // it will use query operator to get required result set
                        var colorPartcity = (from part in parts
                                             where String.Equals(part.Color, colr)
                                             select part.City).Distinct();
                        
                        // it will display results form enumerable
                        foreach (var temp in colorPartcity)
                        {
                            Console.WriteLine(temp);
                        }
                        Console.ReadLine();
                        break;

                    case "3":

                        // it will create enumerable interface to access supplier details
                        IEnumerable<string> supplierNames = suppliers.OrderBy(supp => supp.SName)
                            .Select(supp => supp.SName);
                       
                        // it will display results form enumerable
                        foreach (string name in supplierNames)
                        {
                            Console.WriteLine(name);
                        }
                        Console.ReadLine();
                        break;

                    case "4":

                        // it will get supplier number from user and 
                        // create enumerable interface to join arrays and fetch required result set
                        Console.Write("Please enter Supplier Number: ");
                        suppno = Convert.ToInt32(Console.ReadLine());

                        var partquantity = shipment.Where(ship => ship.SN == suppno)
                            .Select(sh => new { sh.SN, sh.PN, sh.Qty })
                            .Join(parts, s => s.PN, pt => pt.PN,
                            (s, pt) => new { pt.PName, s.Qty });
                        
                        // it will display results form enumerable
                        foreach (var q in partquantity)
                        {                            
                            Console.WriteLine(q);
                        }
                        Console.ReadLine();
                        break;

                    case "0":
                        
                        break;

                }
                
            }

        }

    }
}
