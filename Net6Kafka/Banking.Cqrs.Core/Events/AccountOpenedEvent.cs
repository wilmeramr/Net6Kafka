namespace Banking.Cqrs.Core.Events
{
    public class AccountOpenedEvent : BaseEvent
    {
        public string AccountHolder { get; set; } = string.Empty;
        public string AccounType { get; set; } = string.Empty;
        public DateTime CrearedDate { get; set; }
        public double OpeningBalance { get; set; }

        public AccountOpenedEvent(
            string id,
            string accountHolder,
            string accounType,
            DateTime crearedDate,
            double openingBalance)
            : base(id)
        {
            AccounType = accounType;
            CrearedDate = crearedDate;
            OpeningBalance = openingBalance;
            AccountHolder = accountHolder;
        }


    }
}
