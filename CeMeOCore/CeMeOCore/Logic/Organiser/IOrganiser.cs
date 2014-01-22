using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeOCore.Logic.Organiser
{
    /// <summary>
    /// This is the interface of how an Organiser class should atleast look like.
    /// </summary>
    public interface IOrganiser
    {
        //public string OrganiserID { get; private set; }
        event EventHandler DeadLineChanged;
    }
}
