using Banking.Account.Command.Application.Aggregates;
using Banking.Cqrs.Core.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Command.Application.Features.BankAccounts.Commands.CloseAccount
{
    public class DepositFundCommandHandler : IRequestHandler<CloseAccountCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public DepositFundCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(CloseAccountCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventSourcingHandler.GetById(request.Id);
            aggregate.CloseAccount();
           await _eventSourcingHandler.Save(aggregate);

            return true;
        }
    }
}
