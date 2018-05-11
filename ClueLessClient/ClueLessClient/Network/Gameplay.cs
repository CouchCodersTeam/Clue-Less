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
    }
}
