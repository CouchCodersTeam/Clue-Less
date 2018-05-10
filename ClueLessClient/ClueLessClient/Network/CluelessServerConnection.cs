using System;
using System.Net.Http;
using System.Net.Http.Headers;
using static ClueLessClient.Network.RequestModels;

namespace ClueLessClient.Network
{
    public class CluelessServerConnection
    {
        private static readonly HttpClient client = new HttpClient();

        public Lobbies Lobbies { get; }
        
        private CluelessServerConnection(string host, int port)
        {
            Lobbies = new Lobbies(client);

            UriBuilder builder = new UriBuilder();
            builder.Host = host;
            builder.Port = port;

            client.BaseAddress = builder.Uri;
            client.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static CluelessServerConnection instance = null;

        public static CluelessServerConnection getConnection()
        {
            if (instance == null)
                throw new Exception("Http client not initialized. Call getConnection(string, int) first and once.");
            return instance;
        }

        public static CluelessServerConnection getConnection(string host, int port)
        {
            if (instance != null)
                throw new Exception("Http client already initialized. Call getConnection(string, int) once.");

            instance = new CluelessServerConnection(host, port);
            return instance;
        }

        public bool registerAsPlayer(string playerName)
        {
            var data = new PlayerIdentityModel { PlayerName = playerName};
            var json = Json.toJson(data);

            var response = client.PostAsync("/auth_code", new CluelessJsonContent(json)).Result;

            if (response.IsSuccessStatusCode)
            {
                var player = Json.fromJson<PlayerIdentityModel>(response);

                // Set header data for player
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", player.AuthCode);
                return true;
            }

            return false;
        }

        public bool registerToGame(Lobby lobby)
        {
            client.DefaultRequestHeaders.Remove("Session");
            client.DefaultRequestHeaders.Add("Session", lobby.Id.ToString());
            return true;
        }

    }
}
