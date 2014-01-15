using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.TextRecognition
{
    public class Recognition
    {
        /// <summary>
        /// This private constructor will prevent initializing this class.
        /// </summary>
        private Recognition()
        {
            //Do nothing
        }

        private static HashSet<string> _commandBeginActions = new HashSet<string>() { "I want to",  };
        private static HashSet<string> _commandItSelf = new HashSet<string>() { "plan", "modify", "cancel" };
        private static HashSet<string> _commandOnWhat = new HashSet<string>() { "a meeting", "meeting", "reservation", "a reservation" };



    }
}