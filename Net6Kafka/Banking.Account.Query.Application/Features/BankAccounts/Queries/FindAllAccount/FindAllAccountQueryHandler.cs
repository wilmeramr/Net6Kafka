using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Features.BankAccounts.Queries.FindAllAccount
{
    public class FindAllAccountQueryHandler : IRequestHandler<FindAllAccountQuery, IEnumerable<BankAccount>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public FindAllAccountQueryHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<IEnumerable<BankAccount>> Handle(FindAllAccountQuery request, CancellationToken cancellationToken)
        {
            return await _bankAccountRepository.GetAllAsync();
        }
    }
}
