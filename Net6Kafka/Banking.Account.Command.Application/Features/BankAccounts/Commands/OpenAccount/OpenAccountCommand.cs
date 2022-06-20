﻿using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccounts.Commands.OpenAccount
{
    public class OpenAccountCommand : IRequest<bool>
    {

        public string Id { get; set; } = string.Empty;
        public string AccountHolder { get; set; } = string.Empty;
        public string AccounType { get; set; } = string.Empty;
        public double OpeningBalance { get; set; }
    }
}