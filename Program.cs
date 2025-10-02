﻿using App;
using System.Linq;

/* The following features need to be implemented:

A user needs to be able to register an account CHECKED
A user needs to be able to login to their account CHECKED
A user needs to be able to logout of their account CHECKED
A user needs to be able to upload information about the item they want to trade. CHECKED
A user needs to be able to browse a list of other users items. CHECKED
A user needs to be able to request a trade for other users items. 
A user needs to be able to browse trade requests.
A user needs to be able to accept a trade request.
A user needs to be able to deny a trade request.
A user needs to be able to browse completed requests.

 */

Console.Clear();
Shop shop = new Shop();
List<User> users = new List<User>();
users.Add(new User("123", "123", "123"));
users.Add(new User("mockuser", "1", "1"));
users[0].AddItem(new Item("abc", 23.5, "mock item"));
users[1].AddItem(new Item("banana", 225, "a fruit"));
shop.AddItem(new Item("abc", 23.5, "mock item"));
shop.AddItem(new Item("banana", 225, "a fruit"));

bool running = true;
User? activeUser = null;

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
                Console.Write($"Insert username: ");
                string username = Console.ReadLine() ?? "Guest";

                Console.Write($"Insert password: ");
                string password = Console.ReadLine() ?? "";


                foreach (User u in users)
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
                
                Console.WriteLine($"Welcome {username} ");
                if (activeUser != null)
                {
                    Console.WriteLine("You're now logged in as: " + activeUser.Username);
                }

                Console.ReadLine();
            }
            else
            {

                Console.WriteLine($"Welcome to the Shop");
                Console.WriteLine($"Select an option: ");

                Console.WriteLine($"1. Add Item");
                Console.WriteLine($"2. Display my items");
                Console.WriteLine($"3. Search for items by user");
                Console.WriteLine($"4. Exit");

                string input = Console.ReadLine() ?? "";
                switch (input)
                {
                    case "1":
                        CreateItem();
                        break;
                    case "2":
                        DisplayUserItems();
                        break;
                    case "3":
                        DisplayItemsByUserNameMenu();
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
    string itemName = Console.ReadLine() ?? "";

    Console.WriteLine($"Insert item price: ");
    double price = Convert.ToDouble(Console.ReadLine() ?? "0");

    Console.WriteLine($"Insert item information: ");
    string information = Console.ReadLine() ?? "";

    Item newItem = new Item(itemName, price, information);
    activeUser.AddItem(newItem);
    shop.AddItem(newItem);

    Console.WriteLine($"Item added: {newItem.ToString()}");
    Console.WriteLine($"Press Enter to continue.");
    Console.ReadLine();
}

void DisplayUserItems()
{
    if (activeUser == null)
    {
        Console.WriteLine("No active user.");
    }
    else
    {
        activeUser.DisplayUserItems();
    }

    Console.WriteLine("Press Enter to continue.");
    Console.ReadLine();
}


void DisplayItemsByUserNameMenu()
{
    Console.Clear();
    Console.WriteLine("=== Users ===");

    // checks if user list is empty
    if (users.Count == 0)
    {
        Console.WriteLine("No users found.");
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
        return;
    }

    // Print out all usernames
    for (int i = 0; i < users.Count; i++)
    {
        Console.WriteLine("- " + users[i].Username);
    }

    Console.WriteLine();
    Console.Write("Enter username (leave empty to go back): ");
    string input = (Console.ReadLine() ?? "").Trim();

    // if user presses enter without input, go back
    if (string.IsNullOrEmpty(input))
    {
        return; // tillbaka
    }

    // Find user (case-insensitive)
    User selectedUser = null;
    foreach (var u in users)
    {
        if (string.Equals(u.Username, input, StringComparison.OrdinalIgnoreCase))
        {
            selectedUser = u;
            break;
        }
    }

    // error handling if no user found
    if (selectedUser == null)
    {
        Console.WriteLine("No such user.");
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
        return;
    }

    Console.Clear();
    Console.WriteLine("Items for user: " + selectedUser.Username);
    Console.WriteLine("------------------------------------");

    // fallback code for no items on user
    if (!selectedUser.HasItems)
    {
        Console.WriteLine("This user has no items.");
    }

    // Display items for the selected user
    else
    {
        // foreach loops through the items of the selected user
        foreach (var it in selectedUser.Items)
        {
            Console.WriteLine(it.ToString());
        }
    }

    Console.WriteLine();
    Console.WriteLine("Press Enter to continue.");
    Console.ReadLine();
}