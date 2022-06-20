using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Features.BankAccounts.Queries.FindAccountById
{
    public class FindAccountByIdQuery : IRequest<BankAccount>
    {
        public string Identifer { get; set; } = string.Empty;
    }
}
