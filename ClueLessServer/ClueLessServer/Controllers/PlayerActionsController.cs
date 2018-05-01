using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            AuthResult auth = authorizePlayerAndGame();

            return NotFound();
        }

        [Route("command")]
        [HttpGet]
        public IHttpActionResult RequestCommand()
        {
            AuthResult auth = authorizePlayerAndGame();
            if (auth.result != null)
                return auth.result;

            // TODO: this will require waiting/async behavior
            return NotFound();
        }

        [Route("move")]
        [HttpPost]
        public IHttpActionResult MovePlayer()
        {
            AuthResult auth = authorizePlayerAndGame();
            if (auth.result != null)
                return auth.result;

            return NotFound();
        }

        [Route("suggest")]
        [HttpPost]
        public IHttpActionResult MakeSuggestion()
        {
            AuthResult auth = authorizePlayerAndGame();
            if (auth.result != null)
                return auth.result;

            return NotFound();
        }

        [Route("disprove")]
        [HttpPost]
        public IHttpActionResult DisproveSuggestion()
        {
            AuthResult auth = authorizePlayerAndGame();
            if (auth.result != null)
                return auth.result;

            return NotFound();
        }

        [Route("accuse")]
        [HttpPost]
        public IHttpActionResult MakeAccusation()
        {
            AuthResult auth = authorizePlayerAndGame();
            if (auth.result != null)
                return auth.result;

            return NotFound();
        }

        [Route("solution")]
        [HttpGet]
        public IHttpActionResult GetSolution()
        {
            AuthResult auth = authorizeGame();
            if (auth.result != null)
                return auth.result;

            // Game must be over for this API to be called
            return NotFound();
        }
    }
}
