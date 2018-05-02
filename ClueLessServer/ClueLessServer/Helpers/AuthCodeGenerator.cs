using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLessServer.Helpers
{
    // class is public and separate so that it can be
    // subclassed in testing to be predicable instead of
    // random
    public class AuthCodeGenerator
    {
        // Generates random hexadecimal string
        public virtual string generateAuthCode()
        {
            Random random = new Random();
            int val = random.Next();

            // return hex number
            return val.ToString("X");
        }

    }
}