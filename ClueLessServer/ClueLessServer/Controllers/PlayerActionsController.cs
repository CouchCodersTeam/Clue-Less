using ClueLessClient.Model.Game;
using ClueLessClient.Network;
using ClueLessServer.Helpers;
using ClueLessServer.Models;
using System.Runtime.Serialization;
using System.Web.Http;

namespace ClueLessServer.Controllers
{
    /*
     * This controller is in charge of API calls for taking
     * a turn
     */
    public class PlayerActionsController : ClueLessController
    {
        [Route("state")]
        [HttpGet]
        public IHttpActionResult GetBoardState()
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game;

            if (!game.Started())
                return BadRequest("Game has not started");

            return Ok(game.getGame());
        }

        [Route("command")]
        [HttpGet]
        public IHttpActionResult RequestCommand()
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var gameModel = auth.game;
            var game = gameModel.getGame();
            var player = auth.player;

            bool isTurn = isPlayerTurn(game, player);
            Command command = CommandInterface.GetCommand(gameModel, player);

            if (isTurn)
            {
                if (CommandInterface.GetCommandForPlayer(player.Name) == null)
                {
                    command = new Command { command = CommandType.TakeTurn };
                    CommandInterface.SetCommandForPlayer(player.Name, command);
                }
            }

            return Ok(command);
        }

        [Route("move")]
        [HttpPost]
        public IHttpActionResult MovePlayer([FromBody] Location location)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game.getGame();
            var player = auth.player;
            
            if (!isPlayerTurn(game, player))
                return Unauthorized();

            if (game.movePlayer(player.Name, location))
            {
                Command command = new Command();
                command.command = CommandType.MovePlayer;
                command.data = new CommandData {
                    moveData = new MoveData { playerName = player.Name, location = location }
                };
                CommandInterface.SetCommandForEveryone(auth.game, command);

                return Created("", "");
            }

            return BadRequest("Invalid move");
        }

        [Route("suggest")]
        [HttpPost]
        public IHttpActionResult MakeSuggestion([FromBody] Accusation accusation)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            // TODO: validate accusationData data

            var game = auth.game.getGame();
            var player = auth.player;

            if (!isPlayerTurn(game, player))
                return Unauthorized();

            Command command = new Command();
            command.command = CommandType.SuggestionMade;

            SuggestionData data = new SuggestionData { playerName = player.Name, accusation = accusation };
            var cmdData = new CommandData { suggestData = data };
            var disprovingPlayer = game.makeSuggestion(player.Name, accusation);
            
            if (disprovingPlayer != null)
            {
                data.disprovingPlayer = disprovingPlayer.name;
                var disproveCmd = new Command{ command = CommandType.DisproveSuggestion, data = cmdData };
                CommandInterface.SetCommandForPlayer(disprovingPlayer.name, disproveCmd);

                var waitCmd = new Command { command = CommandType.Wait };
                CommandInterface.SetCommandForPlayer(player.Name, waitCmd);
            }

            command.data = cmdData;
            CommandInterface.SetCommandForEveryone(auth.game, command);

            return Created("", data);
        }

        [Route("disprove")]
        [HttpPost]
        public IHttpActionResult DisproveSuggestion([FromBody] Card card)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            // TODO: authorize call from correct player

            DisproveData result = new DisproveData();
            result.card = card;
            result.card = new Card(Weapon.Pipe);
            result.disprovingPlayer = auth.player.Name;
            var cmdData = new CommandData { disproveData = result };

            // remove player's need to disprove suggestion
            CommandInterface.SetCommandForPlayer(result.disprovingPlayer, null);

            // set command for current player's turn
            var playerName = auth.game.getGame().getPlayerTurn().name;
            var resultCmd = new Command { command = CommandType.DisproveResult, data = cmdData };
            CommandInterface.SetCommandForPlayer(playerName, resultCmd);

            return Created("", "");
        }

        [Route("accuse")]
        [HttpPost]
        public IHttpActionResult MakeAccusation([FromBody] Accusation accusation)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game.getGame();
            var player = auth.player;

            if (!isPlayerTurn(game, player))
                return Unauthorized();

            bool isCorrect = game.makeAccusation(player.Name, accusation);
            AccusationData data = new AccusationData {
                accusation = accusation,
                playerName = player.Name,
                accusationCorrect = isCorrect
            };

            var cmd = new Command {
                command = CommandType.AccusationMade,
                data = new CommandData { accusationData = data }
            };

            CommandInterface.SetCommandForEveryone(auth.game, cmd);

            return Created("", data);
        }

        [Route("finish")]
        [HttpPost]
        public IHttpActionResult EndTurn()
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game.getGame();
            var player = auth.player;

            if (!isPlayerTurn(game, player))
                return Unauthorized();

            game.nextPlayer();

            var cmd = new Command { command = CommandType.TurnEnd };
            CommandInterface.SetCommandForEveryone(auth.game, cmd);

            // remove 'TakeTurn' command from current player
            CommandInterface.SetCommandForPlayer(auth.player.Name, null);

            // Set command for next player's turn
            cmd = new Command { command = CommandType.TakeTurn };
            var nextPlayer = game.getPlayerTurn();
            CommandInterface.SetCommandForPlayer(nextPlayer.name, cmd);

            return Created("", "");
        }

        [Route("solution")]
        [HttpGet]
        public IHttpActionResult GetSolution()
        {
            AuthResult auth = authorizeGame();
            if (auth.result != null)
                return auth.result;

            var game = auth.game;
            if (!game.isEnded)
                return Unauthorized();

            // Game must be over for this API to be called
            return Ok(game.getGame().getSolution());
        }

        private bool isPlayerTurn(Game game, PlayerModel player)
        {
            return game.getPlayerTurn().name.Equals(player.Name);
        }
    }
}
