using System;

//PROGRAM CLASS
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-- Welcome to my shop! --");

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

        //RECEIPT AND DATE
        string orderDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        string[] history = new string[10];
        int receiptNumber = 0001;

        //Start ng Loop
        while (running)
        {
            Console.WriteLine("\n===== MAIN MENU =====");

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
                            AddToCart(products, cart, quantities, ref cartCount);
                            break;

                        case "2"://search by name
                            bool searching = true;
                            while (searching)
                            {
                                Console.Write("\nEnter product name to search (or type 'back' to exit): ");
                                string searchName = Console.ReadLine().ToLower();

                                // Check if user wants to go nack
                                if (searchName == "back")
                                {
                                    searching = false;
                                    break;
                                }

                                // 2. Search Logic
                                bool found = false;
                                foreach (Product p in products)
                                {
                                    if (p.Name.ToLower() == searchName)
                                    {
                                        p.DisplayProduct();
                                        found = true;
                                    }
                                }

                                if (!found)
                                {
                                    Console.WriteLine("Product not found!");
                                }
                            }


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
                            Console.WriteLine("Returning to main menu...");

                            break;
                        default:
                            Console.WriteLine("Invalid Input!");
                            break;

                    }
                    break;//break for case 1

                case "2":
                    if (cartCount == 0)
                    {
                        Console.WriteLine("Cart is empty.");
                    }
                    else
                    {
                        bool inCartMenu = true;
                        while (inCartMenu)
                        {
                            Console.WriteLine("\n--- CART MANAGEMENT MENU ---");
                            Console.WriteLine("1. View Cart\n2. Remove Item\n3. Update Quantity\n4. Clear Cart\n5. Checkout\n6. Back to Main Menu");
                            Console.Write("Input your choice: ");
                            string cartChoice = Console.ReadLine();

                            switch (cartChoice)
                            {
                                case "1":
                                    Console.WriteLine("\n--- YOUR CART ---");
                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        Console.WriteLine($"\n{i + 1}. {cart[i].Name} x{quantities[i]} - P{cart[i].Price * quantities[i]}");
                                    }
                                    break;

                                case "2":
                                    Console.WriteLine("\n--- YOUR CART ---");
                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        Console.WriteLine($"\n{i + 1}. {cart[i].Name} x{quantities[i]} - P{cart[i].Price * quantities[i]}");
                                    }
                                    Console.Write("Enter item number to remove: ");
                                    if (int.TryParse(Console.ReadLine(), out int removeIndex))
                                    {
                                        int index = removeIndex - 1;
                                        if (index >= 0 && index < cartCount)
                                        {
                                            cart[index].RemainingStock += quantities[index];
                                            // Array Shifting Logic
                                            for (int i = index; i < cartCount - 1; i++)
                                            {
                                                cart[i] = cart[i + 1];
                                                quantities[i] = quantities[i + 1];
                                            }
                                            cartCount--;
                                            Console.WriteLine("Item removed.");
                                        }
                                        else { Console.WriteLine("Invalid item number!"); }
                                    }
                                    break;

                                case "3":
                                    Console.WriteLine("\n--- YOUR CART ---");
                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        Console.WriteLine($"\n{i + 1}. {cart[i].Name} x{quantities[i]} - P{cart[i].Price * quantities[i]}");
                                    }
                                    Console.Write("Enter item number to update: ");
                                    if (int.TryParse(Console.ReadLine(), out int updateNum))
                                    {
                                        int index = updateNum - 1;
                                        if (index >= 0 && index < cartCount)
                                        {
                                            Console.Write($"Enter new quantity for {cart[index].Name}: ");
                                            if (int.TryParse(Console.ReadLine(), out int newQty) && newQty > 0)
                                            {
                                                int oldQty = quantities[index];

                                                cart[index].RemainingStock += quantities[index];
                                                if (cart[index].HasEnoughStock(newQty))
                                                {
                                                    quantities[index] = newQty;
                                                    cart[index].DeductStock(newQty);
                                                    Console.WriteLine("Quantity updated!");
                                                }
                                                else
                                                {
                                                    cart[index].RemainingStock -= oldQty;
                                                    Console.WriteLine($"Insufficient stock! Only {cart[index].RemainingStock} left.");
                                                }
                                            }
                                            
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid quantity input!");
                                        }
                                    }
                                    break;


                                case "4"://CLEAR CART
                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        cart[i].RemainingStock += quantities[i];
                                    }
                                    cartCount = 0;
                                    Console.WriteLine("Cart cleared.");
                                    inCartMenu = false;
                                    break;

                                case "5": // CHECKOUT
                                    double bill = 0;
                                    Console.WriteLine("\n--- OFFICIAL RECEIPT ---");
                                    Console.WriteLine($"\nReceipt No: {receiptNumber:D4}\nDate: {DateTime.Now}");
                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        double sub = cart[i].Price * quantities[i];
                                        Console.WriteLine($"\n{cart[i].Name} x{quantities[i]} = {sub}");
                                        bill += sub;
                                    }
                                    if (bill >= 5000) { double disc = bill * 0.1; bill -= disc; Console.WriteLine("Discount: " + disc); }
                                    Console.WriteLine($"\nTOTAL: PHP {bill:N2}");

                                    while (true)
                                    {
                                        Console.Write("\nPayment: ");
                                        if (double.TryParse(Console.ReadLine(), out double pay) && pay >= bill)
                                        {
                                            Console.WriteLine("\nChange: " + (pay - bill));
                                            break;
                                        }
                                        Console.WriteLine("Invalid payment!");
                                    }

                                    if (receiptNumber <= 10)
                                    {
                                        history[receiptNumber - 1] = $"Receipt #{receiptNumber:D4} - Total: PHP {bill:N2} - {DateTime.Now}";
                                        receiptNumber++;
                                    }

                                    Console.WriteLine("\n--- LOW STOCK ALERT ---");
                                    foreach (Product p in products) if (p.RemainingStock <= 5) Console.WriteLine($"ALERT: {p.Name} - {p.RemainingStock} left");

                                    cartCount = 0;
                                    inCartMenu = false;

                                    string rep = "";
                                    while (true)
                                    {
                                        Console.Write("\nAnother transaction? (Y/N): ");
                                        rep = Console.ReadLine().ToUpper();
                                        if (rep == "Y" || rep == "N") break;
                                    }
                                    if (rep == "N")
                                    {
                                        running = false;

                                        Console.WriteLine("Goodbye! :3");
                                        Console.WriteLine("\nPress any key to close...");
                                        Console.ReadKey();
                                    }
                                    break;//break for case 5

                                case "6"://BACK TO MENU
                                    inCartMenu = false;
                                    break;//break for case 6

                                default:
                                    Console.WriteLine("Invalid Input");
                                    break;
                            }
                            if (cartCount == 0 && inCartMenu) inCartMenu = false;
                        }
                    }
                    break;// break for case 2

                case "3":// VIEW HISTORY

                    break;

                case "4":// EXIT
                    
                    break;

                default:
                    Console.WriteLine("Invalid Input!");
                    break;
            }

            //ADD PRODUCT METHOD
            static void AddToCart(Product[] products, Product[] cart, int[] quantities, ref int cartCount)
            {
                Console.Write("\nEnter Product ID to add: ");
                int inputid;

                if (!int.TryParse(Console.ReadLine(), out inputid))
                {
                    Console.WriteLine("Invalid input!");
                    return; // Ginamit ang return imbes na continue
                }

                Product selectedProduct = null;
                foreach (Product p in products)
                {
                    if (p.ID == inputid)
                    {
                        selectedProduct = p;
                        break;
                    }
                }

                if (selectedProduct == null)
                {
                    Console.WriteLine("Invalid product!");
                    return;
                }

                if (!selectedProduct.HasEnoughStock(1))
                {
                    Console.WriteLine("Out of stock!");
                    return;
                }

                Console.Write("How many?: ");
                int inputqty;

                if (!int.TryParse(Console.ReadLine(), out inputqty) || inputqty <= 0)
                {
                    Console.WriteLine("Invalid quantity!");
                    return;
                }

                if (!selectedProduct.HasEnoughStock(inputqty))
                {
                    Console.WriteLine("Not enough stock available.");
                    return;
                }

                int existing = -1;
                for (int i = 0; i < cartCount; i++)
                {
                    if (cart[i].ID == selectedProduct.ID)
                    {
                        existing = i;
                        break;
                    }
                }

                if (existing != -1)
                {
                    quantities[existing] += inputqty;
                    selectedProduct.DeductStock(inputqty);
                    Console.WriteLine("Updated existing item in cart");
                }
                else
                {
                    if (cartCount >= 5)
                    {
                        Console.WriteLine("Cart is full.");
                    }
                    else
                    {
                        cart[cartCount] = selectedProduct;
                        quantities[cartCount] = inputqty;
                        cartCount++;
                        selectedProduct.DeductStock(inputqty);
                        Console.WriteLine("Added to cart!");
                    }
                }
            }

        }
    }
}
