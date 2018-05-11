using ClueLessClient.Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Network
{
    public class Gameplay
    {
        // circular dependency
        private HttpClient client;

        public Gameplay(HttpClient client)
        {
            this.client = client;
        }

        public List<Card> GetPlayerHand()
        {
            var response = client.GetAsync("/cards").Result;
            if (response.IsSuccessStatusCode)
            {
                var body = response.Content.ReadAsStringAsync().Result;
                return Json.fromJson<List<Card>>(response);
            }

            return null;
        }

        public Game GetState()
        {
            var response = client.GetAsync("/state").Result;
            if (response.IsSuccessStatusCode)
            {
                var body = response.Content.ReadAsStringAsync().Result;
                return Json.fromJson<Game>(response);
            }
            return null;
        }

        public Command WaitForCommand()
        {
            var response = client.GetAsync("/command").Result;
            if (response.IsSuccessStatusCode)
            {
                return Json.fromJson<Command>(response);
            }
            return null;
        }

        // Requests to move active player's location to 'location'
        public bool MovePlayerTo(Location location)
        {
            var json = Json.toJson(location);
            var response = client.PostAsync("/move", new CluelessJsonContent(json)).Result;
            return response.IsSuccessStatusCode;
        }

        // returns 'null' if no other player can disprove
        public SuggestionResult MakeSuggestion(Accusation accusation)
        {
            var json = Json.toJson(accusation);
            var response = client.PostAsync("/suggest", new CluelessJsonContent(json)).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json.fromJson<SuggestionResult>(response);
            }
            return null;
        }

        // returns true if request is accepted, false on network error
        public bool DisproveSuggestion(Card proof)
        {
            var json = Json.toJson(proof);
            var response = client.PostAsync("/disprove", new CluelessJsonContent(json)).Result;
            return response.IsSuccessStatusCode;
        }

        // returns 'null' on network failure, otherwise returns 'true' if
        // accusation was correct or 'false' if accusation was incorrect
        public bool? MakeAccusation(Accusation accusation)
        {
            var json = Json.toJson(accusation);
            var response = client.PostAsync("/accuse", new CluelessJsonContent(json)).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json.fromJson<bool>(response);
            }
            return null;
        }

        public bool EndTurn()
        {
            var response = client.PostAsync("/finish", null).Result;
            return response.IsSuccessStatusCode;
        }

        public Accusation GetSolution()
        {
            var response = client.GetAsync("/solution").Result;
            if (response.IsSuccessStatusCode)
            {
                return Json.fromJson<Accusation>(response);
            }
            return null;

        }
    }
}
