namespace App;

class User
{
    public string Username { get; set; }
    public string Email { get; set; }
    private string Password { get; set; }

    List<Item> UserItems { get; set; } // list of items instantiated in constructor

    public IReadOnlyList<Item> Items => UserItems; // read only list of items
    public bool HasItems => UserItems.Count > 0; // if items more the 0 true

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
        UserItems = new List<Item>();
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == Password;
    }

    public override string ToString()
    {
        return $"Name: {Username}, Email: {Email}";
    }

    public void AddItem(Item item)
    {
        UserItems.Add(item);
    }

   public void DisplayUserItems()
    {
        if (UserItems.Count == 0)
        {
            Console.WriteLine("No items.");
            return;
        }

        foreach (var userItem in UserItems)
        {
            Console.WriteLine(userItem.ToString());
        }
    }
}