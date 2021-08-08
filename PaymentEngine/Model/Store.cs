using System.Xml.Serialization;

namespace PaymentEngine.Model {
	[XmlRoot(ElementName="store")]
	public class Store {
		[XmlElement(ElementName="withdrawals")]
		public Withdrawals Withdrawals { get; set; }
		[XmlElement(ElementName="withdrawal-statuses")]
		public WithdrawalStatuses WithdrawalStatuses { get; set; }
		[XmlElement(ElementName="withdrawal-groups")]
		public WithdrawalGroups WithdrawalGroups { get; set; }
		[XmlElement(ElementName="allocations")]
		public Allocations Allocations { get; set; }
		[XmlElement(ElementName="allocation-statuses")]
		public AllocationStatuses AllocationStatuses { get; set; }
		[XmlElement(ElementName="transactions")]
		public Transactions Transactions { get; set; }
		[XmlElement(ElementName="transaction-types")]
		public TransactionTypes TransactionTypes { get; set; }
		[XmlElement(ElementName="accounts")]
		public Accounts Accounts { get; set; }
		[XmlElement(ElementName="account-types")]
		public AccountTypes AccountTypes { get; set; }
		[XmlElement(ElementName="customers")]
		public Customers Customers { get; set; }
		[XmlElement(ElementName="terminals")]
		public Terminals Terminals { get; set; }
		[XmlElement(ElementName="terminal-maps")]
		public TerminalMaps TerminalMaps { get; set; }
	}
}