using Banking.Account.Command.Application.Aggregates;
using Banking.Cqrs.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccounts.Commands.DepositFund
{
    public class WithdrawnFundCommandHandler : IRequestHandler<DepositFundCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public WithdrawnFundCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(DepositFundCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventSourcingHandler.GetById(request.Id);
            aggregate.DepositFunds(request.Amount);
            await _eventSourcingHandler.Save(aggregate);

            return true;
        }
    }
}
