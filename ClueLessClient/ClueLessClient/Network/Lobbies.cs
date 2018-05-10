using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ClueLessClient.Network.RequestModels;

namespace ClueLessClient.Network
{
    public class Lobbies
    {
        // circular dependency
        private HttpClient client;

        public Lobbies(HttpClient client)
        {
            this.client = client;
        }

        public List<Lobby> GetLobbies()
        {
            var response = client.GetAsync("/games").Result;

            if (response.IsSuccessStatusCode)
            {
                var lobbies = Json.fromJson<List<Lobby>>(response);

                return lobbies;
            }

            return null;
        }

        public Lobby CreateLobby()
        {
            var response = client.PostAsync("/games", null).Result;

            if (response.IsSuccessStatusCode)
            {
                var lobby = Json.fromJson<Lobby>(response);

                return lobby;
            }

            return null;
        }

        public bool JoinLobby(Lobby lobby)
        {
            var response = client.PostAsync("/games/" + lobby.Id, null).Result;
            return response.IsSuccessStatusCode;
        }

        public bool WaitForGameStart()
        {
            HttpResponseMessage response;
            do
            {
                // TODO: Need to be able to break out of this loop in order to 'Leave Game'
                response = client.GetAsync("/games/begin").Result;

            } while (response.StatusCode == HttpStatusCode.RequestTimeout);

            return response.IsSuccessStatusCode;
        }

        public bool StartGame()
        {
            var response = client.PostAsync("/games/begin", null).Result;
            return response.IsSuccessStatusCode;
        }

        public bool LeaveGame()
        {
            return false;
        }
    }
}
