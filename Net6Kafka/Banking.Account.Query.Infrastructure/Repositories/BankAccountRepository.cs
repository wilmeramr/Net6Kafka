﻿using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Domain;
using Banking.Account.Query.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Banking.Account.Query.Infrastructure.Repositories
{
    public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(MySqlDbContext context) : base(context)
        {
        }

        public async Task DeleteByIdentifier(string identifier)
        {
            var bankAccount = await _context.BankAccounts!.Where(x => x.Identifier == identifier).FirstOrDefaultAsync();
            if (bankAccount is null)
                throw new Exception($"no se puede eliminar a la cuenta bancaria con el id {identifier}");

            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();

        }

        public async Task DepositBankAccountByIdentifier(BankAccount bankAccount)
        {
            var account = await _context.BankAccounts!.Where(x => x.Identifier == bankAccount.Identifier).FirstOrDefaultAsync();
            if (account is null)
                throw new Exception($"no se puede actualizar a la cuenta bancaria con el id {bankAccount.Identifier}");

            account.Balance += bankAccount.Balance;
            await UpdateAsync(account);
        }

        public async Task<IEnumerable<BankAccount>> FindByAccountHolder(string accountHolder)
        {
            return await _context.BankAccounts.Where(x => x.AccountHolder == accountHolder).ToListAsync();
        }

        public async Task<BankAccount> FindByAccountIdentifier(string identifier)
        {
            return await _context.BankAccounts.Where(x => x.Identifier == identifier).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<BankAccount>> FindByBalanceGreaterThan(double balance)
        {
            return await _context.BankAccounts.Where(x => x.Balance > balance).ToListAsync();

        }

        public async Task<IEnumerable<BankAccount>> FindByBalanceLessThan(double balance)
        {
            return await _context.BankAccounts.Where(x => x.Balance < balance).ToListAsync();

        }

        public async Task WithdrawnBankAccountByIdentifier(BankAccount bankAccount)
        {
            var account = await _context.BankAccounts!.Where(x => x.Identifier == bankAccount.Identifier).FirstOrDefaultAsync();
            if (account is null)
                throw new Exception($"no se puede actualizar a la cuenta bancaria con el id {bankAccount.Identifier}");

            if (account.Balance < bankAccount.Balance)
                throw new Exception($" el balance de la cuenta es menor que el dinero desae retirar para {bankAccount.Id}");
         
            account.Balance -= bankAccount.Balance;
            await UpdateAsync(account);
        }
    }
}
