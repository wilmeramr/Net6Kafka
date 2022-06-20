﻿using Banking.Account.Command.Application.Aggregates;
using Banking.Cqrs.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccounts.Commands.OpenAccount
{
    public class OpenAccountCommandHandler : IRequestHandler<OpenAccountCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public OpenAccountCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(OpenAccountCommand request, CancellationToken cancellationToken)
        {
            var aggregate = new AccountAggregate(request);

            await _eventSourcingHandler.Save(aggregate);

            return true;
        }
    }
}
