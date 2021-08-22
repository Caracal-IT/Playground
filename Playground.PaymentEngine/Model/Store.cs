using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
	[XmlRoot(ElementName="store")]
	public class Store {
		[XmlArray("withdrawals")]
		[XmlArrayItem("withdrawal")]
		public List<Withdrawal> Withdrawals { get; set; }
		
		[XmlArray("withdrawal-statuses")]
		[XmlArrayItem("withdrawal-status")]
		public List<WithdrawalStatus> WithdrawalStatuses { get; set; }
		
		[XmlArray("withdrawal-groups")]
		[XmlArrayItem("withdrawal-group")]
		public List<WithdrawalGroup> WithdrawalGroups { get; set; }
		
		[XmlArray("rule-histories")]
		[XmlArrayItem("rule-history")]
		public List<RuleHistory> RuleHistories { get; set; } = new();
		
		[XmlArray("allocations")]
		[XmlArrayItem("allocation")]
		public List<Allocation> Allocations { get; set; }
		
		[XmlArray("allocation-statuses")]
		[XmlArrayItem("allocation-status")]
		public List<AllocationStatus> AllocationStatuses { get; set; }
		
		[XmlArray("transactions")]
		[XmlArrayItem("transaction")]
		public List<Transaction> Transactions { get; set; }
		
		[XmlArray("transaction-types")]
		[XmlArrayItem("transaction-type")]
		public List<TransactionType> TransactionTypes { get; set; }
		
		[XmlArray("accounts")]
		[XmlArrayItem("account")]
		public List<Account> Accounts { get; set; }
		
		[XmlArray("account-types")]
		[XmlArrayItem("account-type")]
		public List<AccountType> AccountTypes { get; set; }
		
		[XmlArray("customers")]
		[XmlArrayItem("customer")]
		public List<Customer> Customers { get; set; }
		
		[XmlArray("terminals")]
		[XmlArrayItem("terminal")]
		public List<Terminal> Terminals { get; set; }
		
		[XmlArray("terminal-maps")]
		[XmlArrayItem("terminal-map")]
		public List<TerminalMap> TerminalMaps { get; set; }
		
		[XmlArray("terminal-results")]
		[XmlArrayItem("terminal-result")]
		public List<TerminalResult> TerminalResults { get; set; }
	}
}