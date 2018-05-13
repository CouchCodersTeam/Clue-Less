using ClueLessClient.Model.Game;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClueLessClient.Network
{
    public class Gameplay
    {
        // circular dependency
        private HttpClient client;
        private Command lastSeenCommand;

        public Gameplay(HttpClient client)
        {
            this.client = client;
            lastSeenCommand = null;
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

        // For testing purpose ONLY!!
        public void TestOnlySetLastSeenCommand(Command command)
        {
            lastSeenCommand = command;
        }

        public Command WaitForCommand()
        {
            return WaitForCommandAsync().Result;
        }

        public async Task<Command> WaitForCommandAsync()
        {
            // implement polling
            do
            {
                var response = client.GetAsync("/command").Result;
                if (response.IsSuccessStatusCode)
                {
                    Command command = Json.fromJson<Command>(response);
                    if (command != null && command.command != CommandType.Wait)
                    {
                        if ((lastSeenCommand == null) ||
                            !lastSeenCommand.Equals(command))
                        {
                            lastSeenCommand = command;
                            return command;
                        }
                        else if (command.command == CommandType.TakeTurn)
                        {
                            // this is a repeated command that's okay to break on
                            return command;
                        }

                    }

                    await Task.Delay(3000);
                }
                else
                {
                    break;
                }

            } while (true);

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
        public DisproveData MakeSuggestion(Accusation accusation)
        {
            var json = Json.toJson(accusation);
            var response = client.PostAsync("/suggest", new CluelessJsonContent(json)).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = Json.fromJson<SuggestionData>(response);
                if (data.disprovingPlayer == null)
                    return null;

                // Else, wait for disprove command
                var disproveResponse = WaitForCommand();

                if (disproveResponse.command == CommandType.DisproveResult)
                    return disproveResponse.data.disproveData;
                else
                    return null;
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
        public AccusationData MakeAccusation(Accusation accusation)
        {
            var json = Json.toJson(accusation);
            var response = client.PostAsync("/accuse", new CluelessJsonContent(json)).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json.fromJson<AccusationData>(response);
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
