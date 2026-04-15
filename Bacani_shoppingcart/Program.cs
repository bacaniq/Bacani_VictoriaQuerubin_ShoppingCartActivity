using System;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-- Welcome to my shop --");

        //Array
        Product[] products = new Product[7];

        products[0] = new Product { ID = 1, Name = "Candy", Price = 10, RemainingStock = 5 };
        products[1] = new Product { ID = 2, Name = "Cookie", Price = 20, RemainingStock = 3 };
        products[2] = new Product { ID = 3, Name = "Juice", Price = 15, RemainingStock = 4 };
        products[3] = new Product { ID = 4, Name = "Tablet", Price = 8000, RemainingStock = 5 };
        products[4] = new Product { ID = 5, Name = "Headset", Price = 1500, RemainingStock = 12 };
        products[5] = new Product { ID = 6, Name = "FLower", Price = 500, RemainingStock = 6 };
        products[6] = new Product { ID = 7, Name = "Teddy Bear", Price = 900, RemainingStock = 15 };

        bool running = true;


        //Start ng Loop
        while (running)
        {
            Console.WriteLine("\n===== MENU =====");

            foreach (Product p in products)
            {
                p.DisplayProduct(); //calling the method
            }

            Console.Write("\nEnter Product ID (0 = stop): ");
            int inputid;

            if (!int.TryParse(Console.ReadLine(), out inputid))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            // EXIT
            if (inputid == 0)
            {
                break;
            }

            // FIND PRODUCT
            Product selectedProduct = null;

            foreach (Product p in products)
            {
                if (p.ID == inputid)
                {
                    selectedProduct = p;
                    break;
                }
            }

            //If not existing ang product ID
            if (selectedProduct == null)
            {
                Console.WriteLine("Invalid product!");
                continue;
            }
            
        } // end of loop
    }
}

