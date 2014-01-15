using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Range
{
    public class ProposalDateRange : DateRange
    {
        public ProposalDateRange(DateTime start, DateTime end) : base(start, end)
        { }
        
        public void ModifyStartDateTime(DateTime start, double duration)
        {
            this.Start = start;
            this.End = start.AddSeconds(duration);
        }
    }
}