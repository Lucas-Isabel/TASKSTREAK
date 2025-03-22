using Core.Models;
using System.Text.RegularExpressions;

public class UserModel : BaseClass
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public UserModel(string username, string email, string password)
    {
        Username = username;

        if (!IsValidEmail(email)) throw new ArgumentException("Email inválido.");
        SetEmail(email);        
        SetPassword(password);
    }

    private void SetEmail(string email)
    {
        Email = email;
    }
    private void SetPassword(string password)
    {
        Password = password;
    }
    public bool ValidatePassword(string password)
    {
        return Password == password;
    }
    private bool IsValidEmail(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$";
        var regex = new Regex(emailPattern);
        return regex.IsMatch(email);
    }

    public bool UpdateEmail(string email)
    {
        if (!IsValidEmail(email)) return false;
        SetEmail(email);
        return true;
    }
}
