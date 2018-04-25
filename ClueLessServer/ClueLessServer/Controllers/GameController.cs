using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClueLessServer.Models;

namespace ClueLessServer.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {

        private Dictionary<long, GameModel> pGames = new Dictionary<long, GameModel>();

        public GameController()
        {
            List<GameModel> games = new List<GameModel>();

            games.Add(new GameModel("Game1"));
            games.Add(new GameModel("Game2"));
            games.Add(new GameModel("Game3"));

            foreach (GameModel game in games)
            {
                pGames.Add(game.Id, game);
            }
        }

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        // /game/getall
        [HttpGet]
        public IEnumerable<GameModel> GetAll()
        {
            return pGames.Values;
        }

        // game/get/{id}
        public ActionResult Get(long id)
        {
            // return new ActionResult(pGames[id]);
            return null;
        }

        /*
        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetById(long id)
        {

        }
        */

    }
}