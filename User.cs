namespace App;

class User : IUser
{
    public string Username { get; set; }
    string Email { get; set; }
    private string Password { get; set; }

    bool LoggedIn { get; set; } = false;

    public User(string username, string email, string password, bool loggedIn = false)
    {
        Username = username;
        Email = email;
        Password = password;
        LoggedIn = loggedIn;
    }

        public bool TryLogin(string username, string password)
    {
        return username == Username && password == Password;
    }
    
    public override string ToString()
    {
        return $"Name: {Username}, Email: {Email}";
    }  
}