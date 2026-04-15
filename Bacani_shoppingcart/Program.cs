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
        int cartCount = 0;
        Product[] cart = new Product[5];
        int[] quantities = new int[5];

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
                Console.WriteLine("Exiting...");
                return;
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

            if (selectedProduct.RemainingStock == 0)
            {
                Console.WriteLine("Out of stock!");
                continue;
            }

            //ASKING FOR QUANTITY
            Console.Write("How many?: ");
            int inputqty;

            //Input validation
            if (!int.TryParse(Console.ReadLine(), out inputqty) || inputqty <= 0)
            {
                Console.WriteLine("Invalid quantity!");
                continue;
            }

            //checks if mas mataas ang quantity sa stock available
            if (inputqty > selectedProduct.RemainingStock)
            {
                Console.WriteLine("Not enough stock available.");
                continue;
            }

            // ADD TO CART LOGIC
            int existing = -1;
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].ID == selectedProduct.ID)
                {
                    existing = i;
                    break;
                }
            }

            //checks if item already exists in the cart
            if (existing != -1)
            {
                quantities[existing] += inputqty;
                selectedProduct.DeductStock(inputqty); // Uses method in Product class
                Console.WriteLine("Updated existing item in cart");
            }
            else
            {
                if (cartCount >= 5) //Checks if the items in the cart reaches the limit of 5
                {
                    Console.WriteLine("Cart is full.");
                }
                else
                {
                    // Store the selected product object into the cart array
                    cart[cartCount] = selectedProduct;

                    // Store the quantity entered by the user into the quantities array
                    quantities[cartCount] = inputqty;
                    cartCount++;

                    selectedProduct.DeductStock(inputqty);
                    Console.WriteLine("Added to cart!");
                }
            }
        } // end of loop
    }
}

