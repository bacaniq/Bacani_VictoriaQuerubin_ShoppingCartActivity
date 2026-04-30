using System;

//PROGRAM CLASS
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-- Welcome to my shop --");

        //Array
        Product[] products = new Product[7];

        products[0] = new Product { ID = 1, Name = "Candy", Price = 10, RemainingStock = 5, Category = "Snacks" };
        products[1] = new Product { ID = 2, Name = "Cookie", Price = 20, RemainingStock = 3, Category = "Snacks" };
        products[2] = new Product { ID = 3, Name = "Juice", Price = 15, RemainingStock = 4, Category = "Beverages" };
        products[3] = new Product { ID = 4, Name = "Tablet", Price = 8000, RemainingStock = 5, Category = "Electronics" };
        products[4] = new Product { ID = 5, Name = "Headset", Price = 1500, RemainingStock = 12, Category = "Electronics" };
        products[5] = new Product { ID = 6, Name = "Lego", Price = 500, RemainingStock = 6, Category = "Toys" };
        products[6] = new Product { ID = 7, Name = "Teddy Bear", Price = 900, RemainingStock = 15, Category = "Toys" };

        bool running = true;
        int cartCount = 0;
        Product[] cart = new Product[5];
        int[] quantities = new int[5];

        double totalBill = 0;

        //Start ng Loop
        while (running)
        {
            Console.WriteLine("\n===== MENU =====");

            Console.WriteLine("1. Buy Product\n2. Cart Management\n3. View History\n4. Exit");
            Console.Write("Select Option: ");
            string choice = Console.ReadLine(); //user input

            switch (choice) 
            {
                //BUY PRODUCT MENU
                case "1":
                    Console.WriteLine("\n--- BUY PRODUCTS ---");
                    Console.WriteLine("1. View All Products\n2. Search Product by Name\n3. Filter by Category\n4. Back to Menu");
                    Console.Write("Choice: ");
                    string browseChoice = Console.ReadLine();// user input

                    switch (browseChoice) 
                    {
                        case "1"://view all products
                            foreach (Product p in products)
                            {
                                p.DisplayProduct(); //calling the method
                            }
                            break;
   
                        case "2":

                            break;
                        case "3": //filter by category
                            bool filtering = true;
                            while (filtering)
                            {
                                Console.WriteLine("\n== FILTER CATEGORY ==");
                                Console.WriteLine("1. Snacks\n2. Beverages\n3. Electronics\n4. Toys");
                                Console.Write("Enter category to filter (or type 'back' to exit): ");

                                string searchCategory = Console.ReadLine().ToLower();

                                if (searchCategory == "back")
                                {
                                    filtering = false;
                                    break;
                                }

                                string selectedCategory = "";
                                switch (searchCategory)
                                {
                                    case "1": selectedCategory = "Snacks"; break;
                                    case "2": selectedCategory = "Beverages"; break;
                                    case "3": selectedCategory = "Electronics"; break;
                                    case "4": selectedCategory = "Toys"; break;
                                    default:
                                        Console.WriteLine("Invalid category! Try again.");
                                        continue; // Babalik sa taas ng while loop
                                }
                                //display products
                                Console.WriteLine($"\n--- {selectedCategory} Items ---");
                                foreach (Product p in products)
                                {
                                    if (p.Category == selectedCategory)
                                    {
                                        p.DisplayProduct();
                                    }
                                }
                            }

                            break;
                        case "4":
                            break;
                        default: Console.WriteLine("Invalid Input!"); 
                            break;

                    }
                    break;
                    case "2":
                    break;
                    case "3":

                    break;
                    case "4":
                        Console.WriteLine("Returning to main menu...");
                    break;

            }

            //add product
            Console.Write("\nEnter Product ID: ");
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
            string choice2 = "";

            while (true)
            {
                Console.Write("\nAdd more items? (Y/N): ");
                choice2 = Console.ReadLine().ToUpper();

                if (choice2 == "Y" || choice2 == "N")
                {
                    break; // Valid input, exit this small loop
                }

                Console.WriteLine("Invalid input! Please type 'Y' for Yes or 'N' for No.");
            }

            if (choice2 == "N") running = false;

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
