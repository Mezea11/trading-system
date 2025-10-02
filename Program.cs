﻿using App;


/* The following features need to be implemented:

A user needs to be able to register an account CHECKED
A user needs to be able to login to their account CHECKED
A user needs to be able to logout of their account CHECKED
A user needs to be able to upload information about the item they want to trade. CHECKED
A user needs to be able to browse a list of other users items. CHECKED
A user needs to be able to request a trade for other users items. CHECKED
A user needs to be able to browse trade requests. CHECKED
A user needs to be able to accept a trade request. CHECKED
A user needs to be able to deny a trade request. CHECKED
A user needs to be able to browse completed requests. CHECKED

 */

Console.Clear();
List<User> users = new List<User>();
List<Trade> trades = new List<Trade>();
int nextTradeId = 1;


users.Add(new User("123", "123", "123"));
users.Add(new User("mockuser", "1", "1"));
users[0].AddItem(new Item("abc", 23.5, "mock item"));
users[1].AddItem(new Item("banana", 225, "a fruit"));

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

                Console.WriteLine($"Welcome to the Trading System.");
                Console.WriteLine($"Select an option: ");

                Console.WriteLine($"1. Add Item");
                Console.WriteLine($"2. Display my items");
                Console.WriteLine($"3. Search for items by user");
                Console.WriteLine($"4. Request a trade");            
                Console.WriteLine($"5. Browse my trade requests");    
                Console.WriteLine($"6. Accept a trade request");      
                Console.WriteLine($"7. Deny a trade request");        
                Console.WriteLine($"8. Browse completed trades");     
                Console.WriteLine($"9. Logout");

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
                        RequestTrade();
                        break;               
                    case "5":
                        DisplayTradeRequests();
                        break;       
                    case "6":
                        AcceptTradeRequest();
                        break;         
                    case "7":
                        DenyTradeRequest();
                        break;          
                    case "8":
                        BrowseCompletedRequests();
                        break;    
                    case "9":
                        activeUser = null;
                        break;            // logout
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
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

void RequestTrade()
{
    Console.Clear();

    // Pick target user
    Console.WriteLine("=== Users (not you) ===");
    for (int i = 0; i < users.Count; i++)
        if (!ReferenceEquals(users[i], activeUser)) // loop through all users and print out all except activeUser
        Console.WriteLine("- " + users[i].Username);

    Console.Write("Enter target username: ");
    string targetName = (Console.ReadLine() ?? "").Trim();

    User target = null; // instantiate null user, just like at the top of app
    for (int i = 0; i < users.Count; i++) // loop through user list
    {
        if (string.Equals(users[i].Username, targetName, StringComparison.OrdinalIgnoreCase) &&
            !ReferenceEquals(users[i], activeUser)) // compare stringInput username == targetname, and if the reference to user is NOT activeUser
        {
            target = users[i]; // set target to the index of the users input
            break;
        }
    }

    if (target == null) // null check for user
    {
        Pause("No such user (or you chose yourself).");
        return; }

    if (!target.HasItems) // check if target has no items
    {
        Pause("That user has no items.");
        return;
    }

    // Choose requested item (from target)
    Console.WriteLine($"\nItems owned by {target.Username}:");
    for (int i = 0; i < target.Items.Count; i++) // loop through targets items (target is the user that you selected above with string input
    {
        Console.WriteLine($"{i + 1}. {target.Items[i]}"); // +1 is fake index for readability so list doesnt start at 0, next display items
    } 

    Console.Write("Pick requested item number: ");
    int requestedIndex = ReadInt(); // ReadInt func is called, simple IntParse

    if (requestedIndex < 1 || requestedIndex > target.Items.Count) // check if index less than 0 or greater than the amount of items in list
    {
        Pause("Cancelled.");
        return;
    }

    Item requested = target.Items[requestedIndex - 1]; // set item to target.Items, -1 to remove fake index

    // Choose offered item (from you)
    Console.WriteLine($"\nYour items:");
    for (int i = 0; i < activeUser.Items.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {activeUser.Items[i]}");
    }

    Console.Write("Pick offered item number: ");
    int offIndex = ReadInt();

    if (offIndex < 1 || offIndex > activeUser.Items.Count)
    {
        Pause("Cancelled.");
        return;
    }

    Item offered = activeUser.Items[offIndex - 1];

    // Create trade
    Trade trade = new Trade
    {
        Id = nextTradeId++, // nextTradeId is defined at the top. set to 1, its to create a new id for index
        FromUser = activeUser,
        ToUser = target,
        OfferedItem = offered,
        RequestedItem = requested,
        Status = TradeStatus.Pending
    };

    trades.Add(trade);

    Console.WriteLine("\nTrade request sent:");
    Console.WriteLine(trade.ToString());
    Console.WriteLine("Press Enter to continue.");
    Console.ReadLine();
}

void DisplayTradeRequests()
{
    Console.Clear();

    Console.WriteLine("=== Pending trade requests for you ===");
    int shown = 0;
    for (int i = 0; i < trades.Count; i++)
    {
        Trade trade = trades[i];
        if (trade.ToUser == activeUser && trade.Status == TradeStatus.Pending)
        {
            Console.WriteLine(trade.ToString());
            shown++;
        }
    }
    if (shown == 0)
    {
        Pause("(none)");
    }
}

void AcceptTradeRequest()
{
    Console.Clear();

    // View of your pending trades
    Console.WriteLine("=== Your pending requests ===");
    for (int i = 0; i < trades.Count; i++)

        if (trades[i].ToUser == activeUser && trades[i].Status == TradeStatus.Pending)
        {
            Console.WriteLine(trades[i].ToString());
        }

    Console.Write("Enter trade id to ACCEPT trade: ");
    int id = ReadInt();

    Trade trade = FindTradeById(id);
    if (trade == null || trade.Status != TradeStatus.Pending || trade.ToUser != activeUser)
    {
        Pause("Invalid trade.");
        return;
    }

    // Make sure both items are still owned by the same users
    if (!OwnsItem(trade.FromUser, trade.OfferedItem) || !OwnsItem(trade.ToUser, trade.RequestedItem))
    {
        trade.Status = TradeStatus.Denied;
        Pause("Trade invalid (items changed). Marked as DENIED.");
        return;
    }

    // Accept trade and set items in user inventories
    bool itemFromUser = trade.FromUser.RemoveItem(trade.OfferedItem);
    bool itemToUser = trade.ToUser.RemoveItem(trade.RequestedItem);
    if (!itemFromUser || !itemToUser)
    {
        trade.Status = TradeStatus.Denied;
        Pause("Trade failed. Marked as Denied.");
        return;
    }
    trade.ToUser.AddItem(trade.OfferedItem);
    trade.FromUser.AddItem(trade.RequestedItem);

    trade.Status = TradeStatus.Completed;
    Pause("Trade accepted and completed!");
}

void DenyTradeRequest()
{
    Console.Clear();

    Console.WriteLine("=== Your pending requests ===");
    
    for (int i = 0; i < trades.Count; i++)
        if (trades[i].ToUser == activeUser && trades[i].Status == TradeStatus.Pending)
        {
            Console.WriteLine(trades[i].ToString());
        }

    Console.Write("Enter trade id to DENY: ");
    int id = ReadInt();

    Trade trade = FindTradeById(id);
    if (trade == null || trade.Status != TradeStatus.Pending || trade.ToUser != activeUser)
    {
        Pause("Invalid trade.");
        return;
    }

    trade.Status = TradeStatus.Denied;
    Pause("Trade denied.");
}

void BrowseCompletedRequests()
{
    Console.Clear();

    Console.WriteLine("=== Your completed trades ===");
    int shown = 0;
    for (int i = 0; i < trades.Count; i++) // loop through trade list
    {
        Trade trade = trades[i]; // check trade index
        if (trade.Status == TradeStatus.Completed &&
           (trade.FromUser == activeUser || trade.ToUser == activeUser)) // check if status is completed and there has been a transaction from: fromUser to: toUser
        {
            Console.WriteLine(trade.ToString()); // call toString and list information on the trade
            shown++; // basic counter for index
        }
    }
    if (shown == 0)
    {
        Console.WriteLine("(none)");
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }
}


// SHORTCUT TO PARSE USER INPUT 
int ReadInt()
{
    string userInputString = Console.ReadLine() ?? "";
    int userInputInt = 0;

    int.TryParse(userInputString, out userInputInt);

    return userInputInt;
}

// PAUSE FUNCTION, TAKES IN STRING PARAM AND SETS CONSOLE.READLINE AFTER. SHORTCUT FUNC FOR BETTER UX
void Pause(string message)
{
    Console.WriteLine(message);
    Console.WriteLine("Press Enter to continue.");
    Console.ReadLine();
}

// Find trade by index, loop through all trades and return trade index when invoked
Trade FindTradeById(int id)
{
    for (int i = 0; i < trades.Count; i++)
        if (trades[i].Id == id)
        {
            return trades[i];
        }
    return null;
}

// boolean that checks if user owns item, referenceEquals used to see if both objects have the same reference
bool OwnsItem(User user, Item it)
{
    for (int i = 0; i < user.Items.Count; i++)
        if (ReferenceEquals(user.Items[i], it))
        {
            return true; 
        }
    return false;
}
