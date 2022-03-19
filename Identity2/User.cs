namespace Identity2;

public class User
{
    public string Login { get; private set; }
    private string _password;
    private Roles _role;

    public enum Roles
    {
        Admin,
        Customer,
        Performer
    }
    

    public User(string login, string password)
    {
        Login = login;
        _password = password;
        _role = Roles.Customer;

    }
    public bool CheckPassword(string pass)
    {
        return pass == _password;
    }

    public void ChangeRole(Roles role)
    {
        _role = role;
    }

    public override string ToString()
    {
        return Login;
    }
}
