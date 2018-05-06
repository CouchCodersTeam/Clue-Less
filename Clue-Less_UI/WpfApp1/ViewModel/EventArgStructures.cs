using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    class EventArgStructures
    {
        public class MoveEventCommand : EventArgs
        {
            public Board_Controller.Person p;
            public Board_Controller.Room from;
            public Board_Controller.Room to;

            public MoveEventCommand(Board_Controller.Person p, Board_Controller.Room from, Board_Controller.Room to)
            {
                this.p = p;
                this.from = from;
                this.to = to;
            }
        }

    }
}
