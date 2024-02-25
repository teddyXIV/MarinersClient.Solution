using Newtonsoft.Json.Linq;
using RestSharp;

namespace MarinersClient.Models;

public class ApiHelper
{
    private static string jwtToken;
    public static void SetToken(string token)
    {
        jwtToken = token;
    }

    private static void AddAuthHeader(RestRequest request)
    {
        if (!string.IsNullOrEmpty(jwtToken))
        {
            request.AddHeader("Authorization", "Bearer {jwtToken}");
        }
        else
        {
            throw new InvalidOperationException("Please sign in first.");
        }
    }

    public static async Task<string> GetAll()
    {
        RestClient client = new RestClient("http://localhost:5185/");
        RestRequest request = new RestRequest($"api/Players", Method.Get);
        RestResponse response = await client.GetAsync(request);
        return response.Content;
    }

    public static async Task<string> Get(int id)
    {
        RestClient client = new RestClient("http://localhost:5185/");
        RestRequest request = new RestRequest($"api/Players/{id}", Method.Get);
        RestResponse response = await client.GetAsync(request);
        return response.Content;
    }

    public static async void Post(string newPlayer)
    {
        RestClient client = new RestClient("http://localhost:5185/");
        RestRequest request = new RestRequest($"api/Players", Method.Post);
        AddAuthHeader(request);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(newPlayer);
        await client.PostAsync(request);
    }

    public static async void Put(int id, string newPlayer)
    {
        RestClient client = new RestClient("http://localhost:5185/");
        RestRequest request = new RestRequest($"api/Players/{id}", Method.Put);
        AddAuthHeader(request);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(newPlayer);
        await client.PutAsync(request);
    }

    public static async void Delete(int id)
    {
        RestClient client = new RestClient("http://localhost:5185/");
        RestRequest request = new RestRequest($"api/Players/{id}", Method.Delete);
        AddAuthHeader(request);
        request.AddHeader("Content-Type", "application/json");
        RestResponse response = await client.DeleteAsync(request);
    }

    public static async void Register(string newUser)
    {
        RestClient client = new RestClient("http://localhost:5185/");
        RestRequest request = new RestRequest($"api/accounts/register", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(newUser);
        await client.PostAsync(request);
    }

    public static async Task<string> SignIn(string user)
    {
        RestClient client = new RestClient("http://localhost:5185/");
        RestRequest request = new RestRequest($"api/accounts/signIn", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(user);

        RestResponse response = await client.PostAsync(request);

        if (response.IsSuccessful)
        {
            JObject jsonResponse = JObject.Parse(response.Content);
            string token = jsonResponse["token"].ToString();
            SetToken(token);
        }
        return response.Content;
    }
}