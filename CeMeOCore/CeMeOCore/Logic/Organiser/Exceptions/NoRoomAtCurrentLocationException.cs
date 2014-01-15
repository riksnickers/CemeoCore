using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Organiser.Exceptions
{
    /// <summary>
    /// Exception for when there are no rooms available at the specific location
    /// </summary>
    public class NoRoomAtCurrentLocationException : Exception
    {
    }
}