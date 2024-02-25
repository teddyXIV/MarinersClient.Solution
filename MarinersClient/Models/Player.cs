using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MarinersClient.Models;
public class Player
{
    public int PlayerId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public double Average { get; set; }
    public double OnBase { get; set; }
    public double Slug { get; set; }
    public int Homerun { get; set; }

    public static List<Player> GetPlayers()
    {
        var apiCallTask = ApiHelper.GetAll();
        var result = apiCallTask.Result;

        JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
        List<Player> playerList = JsonConvert.DeserializeObject<List<Player>>(jsonResponse.ToString());

        return playerList;
    }

    public static Player GetDetails(int id)
    {
        var apiCallTask = ApiHelper.Get(id);
        var result = apiCallTask.Result;

        JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
        Player player = JsonConvert.DeserializeObject<Player>(jsonResponse.ToString());

        return player;
    }

    public static void Post(Player player)
    {
        string jsonPlayer = JsonConvert.SerializeObject(player);
        ApiHelper.Post(jsonPlayer);
    }

    public static void Put(Player player)
    {
        string jsonPlayer = JsonConvert.SerializeObject(player);
        ApiHelper.Put(player.PlayerId, jsonPlayer);
    }

    public static void Delete(int id)
    {
        ApiHelper.Delete(id);
    }
}

