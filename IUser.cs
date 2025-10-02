namespace App;

interface IUser
{
    string Username { get; }
    bool TryLogin(string username, string password);
    string ToString();

}