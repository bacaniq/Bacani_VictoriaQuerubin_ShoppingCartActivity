using System;


//PROGRAM CLASS
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

        double totalBill = 0;

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

            if (!selectedProduct.HasEnoughStock(1)) //used the method
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
            if (!selectedProduct.HasEnoughStock(inputqty))
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
            string choice = "";

            while (true)
            {
                Console.Write("\nAdd more items? (Y/N): ");
                choice = Console.ReadLine().ToUpper();

                if (choice == "Y" || choice == "N")
                {
                    break; // Valid input, exit this small loop
                }

                Console.WriteLine("Invalid input! Please type 'Y' for Yes or 'N' for No.");
            }

            if (choice == "N") running = false;

        } // end of loop


        if (cartCount > 0) //if may laman ang cart then nag exit si user then pprint ang receipt
        {
            // RECEIPT AND TOTAL
            Console.WriteLine("\n===== RECEIPT =====");
            for (int i = 0; i < cartCount; i++)
            {
                double subtotal = cart[i].GetItemTotal(quantities[i]); //called the method
                Console.WriteLine($"{cart[i].Name} x{quantities[i]} = {subtotal}");
                totalBill += subtotal; // Calculate total 
            }


            //DISCOUNT
            if (totalBill >= 5000)
            {
                double discount = totalBill * 0.10;
                totalBill -= discount;
                Console.WriteLine("Discount (10%): " + discount);
                Console.WriteLine("Final Total: " + totalBill);
            }
            else
            {
                Console.WriteLine($"\nTotal Bill: {totalBill:N2}");
            }

            //UPDATED STOCK
            Console.WriteLine("\n--- UPDATED STOCK ---");
            foreach (Product p in products)
            {
                Console.WriteLine($"{p.Name} - {p.RemainingStock} left");

            }

            Console.WriteLine("\nThank you for shopping!! :3");
        }
        
    } 
}

//PRODUCT CLASS
class Product
{

    public int ID;
    public string Name;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine($"{ID}. {Name} - ${Price} - (Stock: {RemainingStock})");

    }
    public double GetItemTotal(int quantity)
    {
        return Price * quantity;
    }

    public bool HasEnoughStock(int quantity)
    {
        return RemainingStock >= quantity;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }
}

