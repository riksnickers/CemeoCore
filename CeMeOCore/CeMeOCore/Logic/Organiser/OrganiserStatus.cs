using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Organiser
{
    public enum OrganiserStatus
    {
        Initializing = 0,
        ConvertingParticipants = 1,
        CheckingAvailabilityInviteees = 2,
        CalculatingEarliestProposition = 3,
        WaitingOnResponses = 4,
        ReCalculatingEarliestProposition = 5,
        FinishedOrganising = 6,
        Error = 7
    }
}