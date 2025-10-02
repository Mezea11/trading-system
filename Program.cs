using App;

/* The following features need to be implemented:

A user needs to be able to register an account CHECKED
A user needs to be able to login to their account CHECKED
A user needs to be able to logout of their account CHECKED
A user needs to be able to upload information about the item they with to trade.
A user needs to be able to browse a list of other users items.
A user needs to be able to request a trade for other users items.
A user needs to be able to browse trade requests.
A user needs to be able to accept a trade request.
A user needs to be able to deny a trade request.
A user needs to be able to browse completed requests.

 */
Shop shop = new Shop();
List<IUser> users = new List<IUser>();
users.Add(new User("123", "123", "123"));
bool running = true;
IUser? activeUser = null;

while (true)
{
    Console.WriteLine($"Welcome to the Trading System.");
    Console.WriteLine($"Select an option: ");
    Console.WriteLine($"1. Login");
    Console.WriteLine($"2. Register");

    switch (Console.ReadLine())
    {
        case "1":
            TradingSystem();
            break;
        case "2":
            Console.WriteLine($"Insert username: ");
            string username = Console.ReadLine() ?? "";

            Console.WriteLine($"Insert email: ");
            string email = Console.ReadLine() ?? "";

            Console.WriteLine($"Insert password: ");
            string password = Console.ReadLine() ?? "";
            users.Add(new User(username, email, password));

            Console.WriteLine($"User registered. Press Enter to continue.");
            Console.ReadLine();
            continue;

        default:
            Console.WriteLine($"Invalid input. Please try again.");
            break;
    }


    void TradingSystem()
    {
        while (running)
        {
            Console.Clear();

            if (activeUser == null)
            {
                Console.WriteLine($"Insert username: ");
                string username = Console.ReadLine() ?? "Guest";

                Console.WriteLine($"Insert password: ");
                string password = Console.ReadLine() ?? "";


                foreach (IUser u in users)
                {
                    if (u.TryLogin(username, password))
                    {
                        activeUser = u;
                        break;
                    }
                }

                if (!users.Contains(activeUser))
                {
                    Console.WriteLine($"Login failed. Press Enter to try again.");
                    Console.ReadLine();
                    continue;
                }
                Console.WriteLine($"Welcome {username} Is logged in: {activeUser != null}");
                Console.ReadLine();
            }
            else
            {

                Console.WriteLine($"Welcome to the Shop");
                Console.WriteLine($"Select an option: ");

                Console.WriteLine($"1. Add Item");
                Console.WriteLine($"2. Remove Item");
                Console.WriteLine($"3. Display All Items");
                Console.WriteLine($"4. Exit");

                string input = Console.ReadLine() ?? "";
                switch (input)
                {
                    case "1":
                        CreateItem();
                        break;

                    case "2":
                        /* RemoveItem(); */
                        break;
                    case "3":
                        DisplayAllItems();
                        break;
                    case "4":
                        activeUser = null;
                        break;
                    default:
                        Console.WriteLine($"Invalid input. Please try again.");
                        break;
                }
            }
        }
    }
}
void CreateItem()
{
    Console.WriteLine($"Insert item name: ");
    string itemName = Console.ReadLine() ?? "Item";

    Console.WriteLine($"Insert item price: ");
    double price = Convert.ToDouble(Console.ReadLine() ?? "0");

    Item newItem = new Item(itemName, price);
    shop.AddItem(newItem);

    Console.WriteLine($"Item added: {newItem.ToString()}");
    Console.WriteLine($"Press Enter to continue.");
    Console.ReadLine();
}

/* void RemoveItem()
{
    Console.WriteLine($"Insert item name to remove: ");
    string itemName = Console.ReadLine() ?? "";

    Item? itemToRemove = null;
    foreach (var item in shop.Items)
    {
        if (item.ItemName == itemName)
        {
            itemToRemove = item;
            break;
        }
    }

    if (itemToRemove != null)
    {
        shop.RemoveItem(itemToRemove);
        Console.WriteLine($"Item removed: {itemToRemove.ToString()}");
    }
    else
    {
        Console.WriteLine($"Item not found.");
    }

    Console.WriteLine($"Press Enter to continue.");
    Console.ReadLine();
} */

void DisplayAllItems()
{
    shop.DisplayAllItems(new Item("", 0));
    Console.WriteLine($"Press Enter to continue.");
    Console.ReadLine();
}

