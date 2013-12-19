using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeOCore.Logic.MeetingOrganiser
{
    /// <summary>
    /// This is the interface of how an Inviter class should atleast look like.
    /// </summary>
    public interface IInviter
    {
        public string InviterID { get; private set; }
    }
}
