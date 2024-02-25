using Newtonsoft.Json;

namespace MarinersClient.Models;

public class User
{
    public string Email { get; set; }
    public string Password { get; set; }

    public static async Task SignIn(User user)
    {
        string jsonAccount = JsonConvert.SerializeObject(user);
        await ApiHelper.SignIn(jsonAccount);
    }

    public static void Register(User user)
    {
        string jsonUser = JsonConvert.SerializeObject(user);
        ApiHelper.Register(jsonUser);
    }
}