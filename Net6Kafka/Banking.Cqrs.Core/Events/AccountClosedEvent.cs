using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Cqrs.Core.Events
{
    public class AccountClosedEvent : BaseEvent
    {
        public AccountClosedEvent(string id) : base(id)
        {
        }
    }
}
